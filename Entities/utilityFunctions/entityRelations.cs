using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class entityRelations
    {
        public static Type parentType(baseEntity entity)
        {
            var et = entity.GetType();
            switch (et.FullName)
            {
                case "Entities.insurance":
                    return null;
                case "Entities.step":
                    return typeof(insurance);
                case "Entities.fieldSet":
                    return typeof(step);
                case "Entities.field":
                    return typeof(fieldSet);
                case "Entities.boxCategory":
                    return null;
                case "Entities.box":
                    return typeof(boxCategory);
                case "Entities.menu":
                    return null;
                case "Entities.post":
                    return null;
                case "Entities.postCategory":
                    return null;
                case "Entities.term":
                    return null;
                case "Entities.category":
                    return typeof(term);
                case "Entities.attribute":
                    return typeof(category);
                case "Entities.dataType":
                    return null;
                case "Entities.dataValue":
                    return typeof(dataType);
            }
            return null;
        }

        public static int parentId(baseEntity entity)
        {
            var et = entity.GetType();
            switch (et.FullName)
            {
                case "Entities.insurance":
                    return 0;
                case "Entities.step":
                    return (entity as step).insuranceId;
                case "Entities.fieldSet":
                    return (entity as fieldSet).stepId;
                case "Entities.field":
                    return (entity as field).fieldSetId;
                case "Entities.boxCategory":
                    return 0;
                case "Entities.box":
                    return (entity as box).boxCategoryId;
                case "Entities.menu":
                    return 0;
                case "Entities.post":
                    return 0;
                case "Entities.postCategory":
                    return 0;
                case "Entities.term":
                    return 0;
                case "Entities.category":
                    return (entity as category).termId;
                case "Entities.attribute":
                    return (entity as attribute).categoryId;
                case "Entities.dataType":
                    return 0;
                case "Entities.dataValue":
                    return (entity as dataValue).dataTypeId;
            }
            return 0;
        }

        public static string parentName(baseEntity entity)
        {
            var et = parentType(entity);
            if (et == null)
                return "";
            return et.FullName.Replace("Entities.", "");
        }


    }
}
