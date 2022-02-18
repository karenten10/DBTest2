using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionBlazor.DataModels {
    public class WorkScheduleDataModel {
        public string ID { get; set; }
        public string Text { get; set; }
    }

    public class PathScope
    {
        // public int Index { get; set; }

        public string PathIdScopeId { get; set; }

        public int PathId { get; set; }
        public string PathName { get; set; }

        public int ScopeId { get; set; }
        public string ScopeName { get; set; }

        public string PathScopeName { get; set; }
    }

    public class PathPlace
    {
        // public int Index { get; set; }

        public string PathIdPlaceId { get; set; }

        public int PathId { get; set; }
        public string PathName { get; set; }

        public int ScopeId { get; set; }
        public string ScopeName { get; set; }

        public int PlaceId { get; set; }
        public string PlaceName { get; set; }

        public string PathScopeName { get; set; }

        public string PathPlaceName { get; set; }
    }

    public class PathPlaceEquipment
    {
        // public int Index { get; set; }

        public string FullId { get; set; }

        public int PathId { get; set; }
        public string PathName { get; set; }

        public int ScopeId { get; set; }
        public string ScopeName { get; set; }

        public int PlaceId { get; set; }
        public string PlaceName { get; set; }

        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }

        public string PathPlaceName { get; set; }
        public string FullName { get; set; }
    }
}
