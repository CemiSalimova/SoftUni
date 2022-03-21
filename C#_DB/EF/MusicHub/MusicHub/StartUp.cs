using MusicHub.Data;
using System;
using System.Linq;
using System.Text;

namespace MusicHub
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new MusicHubDbContext();
            int producerId = 1;
            //Console.WriteLine(ExportAlbumsInfo(context, producerId));
            Console.WriteLine(ExportSongsAboveDuration(context,4));
        }
       public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb = new StringBuilder();
            var albums = context.Producers
                  .Where(x => x.Id== producerId)
                  .Select(p => new 
                  {
                      ProducerName=p.Name,
                      Albums=p.Albums.Select
                      (a=> new 
                      {
                          AlbumName=a.Name,
                          ReleaseDate=a.ReleaseDate,
                          ProducerName=a.Producer.Name,
                          Songs=a.Songs
                          .Select
                          (s=>new
                          {
                              SongName = s.Name,
                              SongPrice=s.Price,
                              SongWriter=s.Writer.Name
                              
                          } ).ToList(),
                          AlbumPrice=a.Price
                      }
                      ).ToList()
                  }
                  )
                  
                  .ToList();

            foreach (var a in albums)
            {
                sb.AppendLine($"{a.ProducerName}");
                foreach (var b in a.Albums.OrderByDescending(d =>d.AlbumPrice))
                {
                    sb.AppendLine($"-AlbumName: {b.AlbumName}");
                    sb.AppendLine($"-RealeaseDate: {b.ReleaseDate.ToString("MM/dd/yyyy")}");
                    sb.AppendLine($"-Songs:");
                    int count = 1;
                    foreach (var c in b.Songs.OrderBy(a=>a.SongName).ThenByDescending(a=>a.SongPrice))
                    {
                       
                        sb.AppendLine($"---#{count}");
                        sb.AppendLine($"---{c.SongName}");
                        sb.AppendLine($"---{c.SongWriter}");
                        sb.AppendLine($"---{c.SongPrice:f2}");
                        count++;

                    }
                    sb.AppendLine($"-AlbumPrice: {b.AlbumPrice:f2}");

                }

            }


            return sb.ToString().TrimEnd();
        }
        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder sb = new StringBuilder();
            var songs = context.Songs.ToList().Where(s => s.Duration.TotalSeconds > duration)
                .Select
                (a => new
                {
                    Songname = a.Name,
                    SongWriter = a.Writer.Name,
                    Performer = a.SongPerformers
                    .Select(sp => sp.Performer.FirstName + " " + sp.Performer.LastName)
                        .FirstOrDefault(),
                    AlbumProducer = a.Album.Producer.Name,
                    SongDuration = a.Duration
                }
                ).ToList();
            int count = 1;
            foreach (var s in songs.OrderBy(s => s.Songname).ThenBy(s => s.SongWriter).ThenBy(s => s.Performer))
            {
                sb.AppendLine($"-Song #{count}");
                sb.AppendLine($"---SongName: {s.Songname}");
                sb.AppendLine($"---Writer: {s.SongWriter}");
                sb.AppendLine($"---Performer: {s.Performer}");
                sb.AppendLine($"---AlbumProducer: {s.AlbumProducer}");
                sb.AppendLine($"---Duration: {s.SongDuration:c}");
                count++;
            }
            return sb.ToString().TrimEnd();
        }
    }
}
