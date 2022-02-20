using Syncfusion.Blazor;
using System.Resources;

namespace InspectionBlazor.Helpers
{
    public class SFLocalizer : ISyncfusionStringLocalizer
    {
        public ResourceManager ResourceManager
        {
            get
            {
                return DBTest.Resources.SfResources.ResourceManager;
            }
        }

        public string GetText(string key)
        {
            return this.ResourceManager.GetString(key);
        }
    }
}
