using SportClassifier.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Data
{
    public interface IUowData
    {
        DbContext Context { get; }
        IRepository<User> Users { get; }

        //IRepository<Field> Fields { get; }
        //IRepository<SatelliteDownload> DownloadedFiles { get; }
        //IRepository<Forecast> Forecasts { get; }
        //IRepository<Coordinate> Coordinates { get; }
        //IRepository<DownloadQueue> DownloadQueue { get; }
        //IRepository<Indices> Indices { get; }

        //IRepository<KeyType> KeyTypes { get; }
        //IRepository<KeyValue> KeyValues { get; }
        //IRepository<Message> Messages { get; }

        //IRepository<AgmRole> AgmRoles { get; }

        //IRepository<AgmPermission> AgmPermissions { get; }

        //IRepository<AgmModule> AgmModules { get; }

        ////IRepository<AgmUserRole> AgmUserRoles { get;  }
        //IRepository<FieldNote> FieldNotes { get; }

        //IRepository<CropType> CropTypes { get; }

        //IRepository<ArticleLink> ArticleLinks { get; }

        //IRepository<Article> Articles { get; }

        //IRepository<Setting> Settings { get; }


        
        //IRepository<PhenologyPlant> PhenologyPlants { get; }
        //IRepository<PhenologyPhase> PhenologyPhases { get; }

        // IRepository<PhenologyPhaseName> PhenologyPhaseNames { get; }
        

        int SaveChanges();
    }
}
