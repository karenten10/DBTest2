using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.Interfaces
{
    public interface IRazorPage
    {
        void NeedRefresh();
        Task NeedInvokeAsync(Action action);
    }
}
