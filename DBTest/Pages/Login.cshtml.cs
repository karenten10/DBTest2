using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Database.Models.Models;
using InspectionBlazor.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InspectionShare.Helpers;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace InspectionBlazor.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly PersonService personService;

        public LoginModel(PersonService personService)
        {
            this.personService = personService;
#if DEBUG
            Username = "httc";
            Password = "24508323";
            PasswordType = "";
#endif
        }
        [BindProperty]
        public string Username { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";
        [BindProperty]
        public string inputValidateCode { get; set; }
        [BindProperty]
        public string checkValidateCode { get; set; }
        [BindProperty]
        public string checkCode { get; set; }
        public string PasswordType { get; set; } = "password";
        public string Msg { get; set; }
        public string base64ValidateCode { get; set; }
        public void OnGet()
        {
            // RefreshCode();
        }

//        public void RefreshCode()
//        {
//            var strbyte = GetValidateCode();
//            var validateCodeImg = BufferToImage(strbyte);
//            base64ValidateCode = ImageToBase64(validateCodeImg, ImageFormat.Png);
//#if DEBUG
//            inputValidateCode = checkValidateCode;
//#else
//            inputValidateCode = "";
//#endif
//        }
        public string ReturnUrl { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) /*&& !string.IsNullOrEmpty(inputValidateCode)*/)
            {
                (bool result, string msg, Person person) = await personService.LoginAsync(Username, Password, true, "", ""/*inputValidateCode.ToLower(), checkCode*/);
                if (result)
                {
                    string returnUrl;

                    // 預設密碼(=帳號)就導到修改密碼頁提醒修改
                    if (Password == Username && !ShareMagicHelper.系統管理員帳號.Contains(Username.ToLower()))
                    {

                        //儀錶板預設帳號 12345 不用跳修改密碼畫面

                        if(Username == "12345")
                        {
                            returnUrl = Url.Content("~/");
                        }
                        else
                        {
                            returnUrl = Url.Content("~/PersonInfoModify");
                        }

                    }
                    else
                    {
                        returnUrl = Url.Content("~/");
                    }

                    //超過三個月沒改密碼跳轉
                    if (await personService.IsChangePasswordAsync(Username) && !ShareMagicHelper.系統管理員帳號.Contains(Username.ToLower()))
                    {
                        returnUrl = Url.Content("~/PersonInfoModify");
                    }

                    try
                    {
                        // 清除已經存在的登入 Cookie 內容
                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    }
                    catch { }

                    #region 加入這個使用者需要用到的 宣告類型 Claim Type
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Sid, person.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, person.Account),
                        new Claim(ClaimTypes.Name, person.Name),
                    };

                    if (ShareMagicHelper.系統管理員帳號.Contains(Username.ToLower()))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                        if (Username.ToLower().Equals("httc"))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, "Httc"));
                        }
                    }
                    else
                    {
                        List<string> menuCodes = await personService.GetAuthorityMenuCodeByPersonAsync(person.Id);
                        foreach (var item in menuCodes)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));
                        }
                    }
                    #endregion

                    #region 建立 宣告式身分識別
                    // ClaimsIdentity類別是宣告式身分識別的具體執行, 也就是宣告集合所描述的身分識別
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    #endregion

                    #region 建立關於認證階段需要儲存的狀態
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        RedirectUri = this.Request.Host.Value
                    };
                    #endregion

                    #region 進行使用登入
                    try
                    {
                        await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    }
                    catch (Exception ex)
                    {
                        string error = ex.Message;
                    }
                    #endregion

                    #region 寫入登入紀錄
                    await personService.LoginHistoryAsync(person.Id);
                    #endregion

                    return LocalRedirect(returnUrl);
                }
                else
                {
                    Msg = msg;
                }
            }
            else
            {
                Msg = "請完整輸入帳號/密碼/驗證碼";
            }

            // RefreshCode();
            return Page();
        }

        private string RandomCode(int length)
        {
            string seed = "0123456789";
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            int index;
            for (int i = 0; i < length; i++)
            {
                index = rand.Next(0, seed.Length);
                sb.Append(seed[index]);
            }
            return sb.ToString();
        }

        //private void PaintInterLine(Graphics g, int num, int width, int height)
        //{
        //    Random r = new Random();
        //    int startX, startY, endX, endY;
        //    for (int i = 0; i < num; i++)
        //    {
        //        startX = r.Next(0, width);
        //        startY = r.Next(0, height);
        //        endX = r.Next(0, width);
        //        endY = r.Next(0, height);
        //        g.DrawLine(new Pen(Brushes.LightSteelBlue), startX, startY, endX, endY);
        //    }
        //}

        //public byte[] GetValidateCode()
        //{
        //    string code = RandomCode(4);
        //    checkValidateCode = code.ToLower();
        //    MemoryStream ms = new MemoryStream();
        //    using (Bitmap map = new Bitmap(60, 32))
        //    {
        //        using (Graphics g = Graphics.FromImage(map))
        //        {
        //            g.Clear(Color.White);
        //            //繪製干擾線
        //            PaintInterLine(g, 10, map.Width, map.Height);
        //            //驗證碼
        //            g.DrawString(code, new Font("Consolas", 16.0F), Brushes.DimGray, new Point(2, 3));
        //        }
        //        map.Save(ms, ImageFormat.Png);
        //    }
        //    return ms.GetBuffer();
        //}

        /// 將 Byte 陣列轉換為 Image。      
        //public static Image BufferToImage(byte[] Buffer)
        //{
        //    if (Buffer == null || Buffer.Length == 0) { return null; }
        //    byte[] data = null;
        //    Image oImage = null;
        //    Bitmap oBitmap = null;
        //    //建立副本
        //    data = (byte[])Buffer.Clone();
        //    try
        //    {
        //        MemoryStream oMemoryStream = new MemoryStream(Buffer);
        //        //設定資料流位置
        //        oMemoryStream.Position = 0;
        //        oImage = Image.FromStream(oMemoryStream);
        //        //建立副本
        //        oBitmap = new Bitmap(oImage);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    //return oImage;
        //    return oBitmap;
        //}

        //public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        // Convert Image to byte[]
        //        image.Save(ms, format);
        //        byte[] imageBytes = ms.ToArray();

        //        // Convert byte[] to base 64 string
        //        string base64String = Convert.ToBase64String(imageBytes);
        //        return base64String;
        //    }
        //}

        //public IActionResult OnGetRefreshCode()
        //{
        //    RefreshCode();
        //    return Page();
        //}
    }
}