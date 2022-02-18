using Database.Models.Models;
using InspectionBlazor.DataModels;
using InspectionBlazor.Services;
using System;
using System.Collections.Generic;

namespace InspectionBlazor.AdapterModels
{
    public class WorkScheduleAdapterModel : ICloneable
    {
        public DateTime WorkDate { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public string FullName { get; set; }
        
        public virtual PatrolGroup Group { get; set; }
     
        public List<long> IdList { get; set; }
        
        public int[] PathIds { get; set; }

        public int[] PathPlaceIds { get; set; }

        public int[] PathPlaceEquipmentIds { get; set; }

        public List<PathScope> PathScopes { get; set; }
        public List<PathPlace> PathPlaces { get; set; }
        public List<PathPlaceEquipment> PathPlaceEquipments { get; set; }

        public List<WorkScheduleOne> WorkList { get; set; }

        
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class WorkScheduleOne 
    {
        public long Id { get; set; }
        public DateTime WorkDate { get; set; }
        public int GroupId { get; set; }
        public int PatrolPathId { get; set; }
        public int PatrolScopeId { get; set; }
        public int PatrolPlaceId { get; set; }
        public int EquipmentId { get; set; }

        public string GroupName { get; set; }
        public string ScopeName { get; set; }
        public string PathName { get; set; }
        public string PlaceName { get; set; }
        public string EquipmentName { get; set; }

        public virtual PatrolGroup Group { get; set; }
        public virtual PatrolPath Path { get; set; }
        public virtual PatrolPlace Place { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
