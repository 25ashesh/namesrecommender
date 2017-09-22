using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NamesRecommender.Models
{
    public class NamesInitializer:DropCreateDatabaseIfModelChanges<NamesContext>
    {
        protected override void Seed(NamesContext context)
        {
            var genders = new List<NameGender> { 
                new NameGender{Gender="Male"},
                new NameGender{Gender="Female"},
                new NameGender{Gender="UniSex"}
            };
            genders.ForEach(p=>context.Genders.Add(p));
            context.SaveChanges();

            var categories = new List<NameCategory> { 
                new NameCategory{Category="Modern"},
                new NameCategory{Category="Popular"},
                new NameCategory{Category="Rare"},
                new NameCategory{Category="Ancient"}
            };
            categories.ForEach(p=>context.Categories.Add(p));
            context.SaveChanges();

            var types = new List<NameType> { 
                new NameType{Type="Religious"},
                new NameType{Type="Literature"},
                new NameType{Type="Abstract"},
                new NameType{Type="Romantic"},
                
            };
            types.ForEach(p=>context.Types.Add(p));
            context.SaveChanges();

            var origins = new List<NameOrigin>
            {
                new NameOrigin{Origin="Hindu"},
                new NameOrigin{Origin="Buddhist"},
                new NameOrigin{Origin="Greek"},
                new NameOrigin{Origin="Roman"},
                new NameOrigin{Origin="Muslim"},
                new NameOrigin{Origin="Arabic"},
                new NameOrigin{Origin="Christian"},
                new NameOrigin{Origin="African"},
            };
            origins.ForEach(p=>context.Origins.Add(p));
            context.SaveChanges();

            var lengths = new List<NameLength>
            {
                new NameLength{Length="Short"},
                new NameLength{Length="Medium"},
                new NameLength{Length="Long"},
            };
            lengths.ForEach(p=>context.Lengths.Add(p));
            context.SaveChanges();

            var names = new List<NameDetail>
            {
                new NameDetail{
                    NameText="Arjun",
                    Meaning="A great warrior in ancient Hindu epic Mahabharata",
                    NamesInfo="Popular among Hindu, this name signifies a great warrior, disciple and follower of truth.",
                    NameGenderId=1,
                    NameCategoryId=4,
                    NameTypeId=1,
                    NameOriginId=1,
                    NameLengthId=2,
                },

                new NameDetail{
                    NameText="Juliet",
                    Meaning="Lady from famous love story of Romeo and Juliet.",
                    NamesInfo="A western name this name implies lady who was true lover.",
                    NameGenderId=2,
                    NameCategoryId=2,
                    NameTypeId=1,
                    NameOriginId=1,
                    NameLengthId=2
                },

                new NameDetail{
                    NameText="Nova",
                    Meaning="A star showing a sudden large increase in brightness and then slowly returning to its original state.",
                    NamesInfo="Pretty unique and abstract this implies heavenly bodies such as star.",
                    NameGenderId=3,
                    NameCategoryId=1,
                    NameTypeId=4,
                    NameOriginId=7,
                    NameLengthId=1
                },
            };
            names.ForEach(p=>context.Names.Add(p));
            context.SaveChanges();
        }
    }
}