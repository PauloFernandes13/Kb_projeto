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

            var subjects = new List<Subject>
            {
                new Subject {Name="Importação de Dados" },
                new Subject {Name="Instalação" },
                new Subject {Name="Base de Dados" },

            };
            subjects.ForEach(s => context.Subjects.Add(s));
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
                new Faq { SubjectID=1,Description= "Install BD Demo",Answer="How to Install BD Demo?", Question="ContactSuport",
                         UrlFaq="http.faq"}
            };

            var clients = new List<Client>
            {
                new Client {JourneyID=1,Name="ESAD",Email="Esad@hotmail.com",Adress="Rua de Matosinhos"},
                new Client {JourneyID=2,Name="FDUL",Email="Flup@gmail.com",Adress="Rua das Oliveiras" },
                new Client {JourneyID=3,Name="Faculdade do Perú",Email="FLPeru@",Adress="Rua de Lima" }
            };


            clients.ForEach(s => context.Clients.Add(s));
            context.SaveChanges();

            var images = new List<Image>
            {
                new Image {ConceptID=1,Name="Foto.png",UrlImage="Content/Foto.png" },
                new Image {ConceptID=2,Name="Image.png",UrlImage="Content/Image.png" }
            };

           
            images.ForEach(s => context.Images.Add(s));
            context.SaveChanges();

            var milestones = new List<Milestone>
            {
                new Milestone {ClientID =1,Name="Onboarding",MilestoneDate=DateTime.Parse("2016-04-21") },
                new Milestone {ClientID =1,Name="Install/Import",MilestoneDate=DateTime.Parse("2016-06-21") },
            };

            milestones.ForEach(s => context.Milestones.Add(s));
            context.SaveChanges();

        }

    }
}