using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SportClassifier.Models;

namespace SportClassifier.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public IDbSet<Field> Fields { get; set; }
        //public IDbSet<SatelliteDownload> DownloadedFiles { get; set; }
        //public IDbSet<Forecast> Forecasts { get; set; }
        //public IDbSet<Coordinate> Coordinates { get; set; }
        //public IDbSet<DownloadQueue> DownloadQueue { get; set; }
        //public IDbSet<Indices> Indices { get; set; }
        //public IDbSet<KeyType> KeyTypes { get; set; }
        //public IDbSet<KeyValue> KeyValues { get; set; }
        //public IDbSet<Message> Messages { get; set; }

        //public IDbSet<AgmRole> AgmRoles { get; set; }

        //public IDbSet<AgmPermission> AgmPermissions { get; set; }

        //public IDbSet<AgmModule> AgmModules { get; set; }

        //public IDbSet<CropType> CropTypes { get; set; }

        //public IDbSet<FieldNote> FieldNotes { get; set; }

        //public IDbSet<ArticleLink> ArticleLinks { get; set; }

        //public IDbSet<Article> Articles { get; set; }

        //public IDbSet<Setting> Settings { get; set; }



        //public IDbSet<PhenologyPlant> PhenologyPlants { get; set; }

        //public IDbSet<PhenologyPhase> PhenologyPhases { get; set; }

        //public IDbSet<PhenologyPhaseName> PhenologyPhaseNames { get; set; }




        //public IDbSet<AgmUserRole> AgmUserRoles { get; set; }






    }
}
