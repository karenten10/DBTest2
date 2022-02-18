using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Syncfusion.Blazor.Navigations;
using System.Collections.Generic;

namespace InspectionBlazor.Helpers {
    public class UserHelper {
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public UserHelper(AuthenticationStateProvider authenticationStateProvider) {
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<(int, string)> GetUserInformationAsync() {
            #region  取得現在登入使用者資訊
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated) {
                return (Convert.ToInt32(user.FindFirst(c => c.Type == ClaimTypes.Sid)?.Value),
                        user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value);
            } else {
                return (-1, string.Empty);
            }
            #endregion
        }

        public async Task<(int, string, string)> GetUserInformation2Async() {
            #region  取得現在登入使用者資訊
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated) {
                return (Convert.ToInt32(user.FindFirst(c => c.Type == ClaimTypes.Sid)?.Value),
                        user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                        user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value);
            } else {
                return (-1, string.Empty, string.Empty);
            }
            #endregion
        }
    }
}
