using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Projeto_KB.Models;

namespace Projeto_KB.DAL
{
    public class KbaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<KbaseContext>
    {
        protected override void Seed(KbaseContext context)
        {
            var journeys = new List<Journey>
            {
                new Journey {Name="Onboarding" },
                new Journey {Name="Install/Import" },
                new Journey {Name="Getting Results" },

            };

            journeys.ForEach(s => context.Journeys.Add(s));
            context.SaveChanges();

            var concepts = new List<Concept>
            {
                new
                Concept {JourneyID=1,Title="Reunião de Arranque",Description="Boot Meeting",Text="Deadlines Implementacion",
                         ContentDate=DateTime.Parse("2016-03-21"),KeyWords="Meetings",UrlContent="http.Content"},
                new
                Concept {JourneyID=2,Title="Installation",Description="Assumptions",Text="Deadlines Installation",
                         ContentDate=DateTime.Parse("2016-04-21"),KeyWords="Installation",UrlContent="http.Content"},
                new
                Concept {JourneyID=3,Title="Generation",Description="Generation",Text="Formation",
                         ContentDate=DateTime.Parse("2016-04-22"),KeyWords="Formation",UrlContent="http.Content"},


            };

            concepts.ForEach(s => context.Concepts.Add(s));
            context.SaveChanges();

            var faqs = new List<Faq>
            {
                new Faq { JourneyID=2, Description= "Install BD Demo",Answer="How to Install BD Demo?", Question="ContactSuport",
                         UrlFaq="http.faq"}
            };


        }








        }

    }
}