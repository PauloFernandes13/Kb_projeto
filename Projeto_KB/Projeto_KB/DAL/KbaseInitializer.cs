﻿using System;
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
                new Subject {Name="Data Importacion" },
                new Subject {Name="Instalacion" },
                new Subject {Name="Database" },
                new Subject {Name="Bullet Calendar" }, 

            };
            subjects.ForEach(s => context.Subjects.Add(s));
            context.SaveChanges();


            var concepts = new List<Concept>
            {
                new
                Concept {JourneyID=1,Title="Reunião de Arranque",Text="Deadlines Implementacion",
                         ContentDate=DateTime.Parse("2016-03-21"),Order="Meetings"},
                new
                Concept {JourneyID=2,Title="Installation",Text="Deadlines Installation",
                         ContentDate=DateTime.Parse("2016-04-21"),Order="Installation"},
                new
                Concept {JourneyID=3,Title="Generation", Text="Formation",
                         ContentDate=DateTime.Parse("2016-04-22"),Order="Formation"},


            };

            concepts.ForEach(s => context.Concepts.Add(s));
            context.SaveChanges();

            var topics = new List<Topic>
            {
                new Topic {Name="General Data" },
                new Topic {Name="Importacion" }
            };

            topics.ForEach(s => context.Topics.Add(s));
            context.SaveChanges();

            var faqs = new List<Faq>
            {
                new Faq { SubjectID=2,TopicID=1,Description= "Install BD Demo",Answer="How to Install BD Demo?", Question="ContactSuport",
                         }
            };

            faqs.ForEach(s => context.Faqs.Add(s));
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
                new Milestone {Name="Onboarding",MilestoneDate=DateTime.Parse("2016-04-21") },
                new Milestone {Name="Install/Import",MilestoneDate=DateTime.Parse("2016-06-21") },
            };

            milestones.ForEach(s => context.Milestones.Add(s));
            context.SaveChanges();

        }

    }
}