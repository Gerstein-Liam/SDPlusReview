using DOMAIN;
namespace WPF.Models.Mappers
{
    public class ModelsMapper : IModelsMapper
    {
        #region FromDtoToModel
        public List<Owner> map(List<OwnerDto> s)
        {
            List<Owner> list = new List<Owner>();

            if (s != null)
            {

                foreach (var owners in s)
                {
                    list.Add(map(owners));
                }
            }
            return list;
        }


        public Owner map(OwnerDto s)
        {

            Owner owner = new Owner();
            owner.ID = s.ID;
            owner.OwnerName = s.OwnerName;
            if (s.Properties != null)
            {
                foreach (var propertyDto in s.Properties)
                {
                    owner.Properties.Add(map(propertyDto));
                }
            }
            return owner;
        }

        public Property map(PropertyDto s)
        {
            Property property = new Property();
            property.ID = s.ID;
            property.OwnerID = s.OwnerID;
            property.PropertyName = s.PropertyName;
            property.OwnerName = s.OwnerName;
            property.Adress=s.Adress;
            property.ZIP = s.ZIP;   
            property.SunRate = new SunRate();
            if (s.SunRate != null) {

                property.SunRate.January = s.SunRate.January;
                property.SunRate.February = s.SunRate.February;
                property.SunRate.Mars = s.SunRate.Mars;
                property.SunRate.April = s.SunRate.April;
                property.SunRate.May = s.SunRate.May;
                property.SunRate.Jun = s.SunRate.Jun;
                property.SunRate.July = s.SunRate.July;
                property.SunRate.August = s.SunRate.August;
                property.SunRate.September = s.SunRate.September;
                property.SunRate.October = s.SunRate.October;
                property.SunRate.November = s.SunRate.November;
                property.SunRate.December = s.SunRate.December;

            }
           


            if (s.Vertices != null)
            {

                foreach (MapPointDto item in s.Vertices)
                {
                    if (item != null) property.Vertices.Add(map(item));


                }

            }
            return property;
        }

        public Map_Point map(MapPointDto s)
        {
            Map_Point map = new Map_Point();
            map.X = s.X;
            map.Y = s.Y;
            map.GeoIndex = s.GeoIndex;
            return map;
        }
        #endregion

        #region FromModelToDto
        public List<OwnerDto> map(List<Owner> s)
        {
            List<OwnerDto> list = new List<OwnerDto>();

            if (s != null)
            {

                foreach (var owners in s)
                {
                    list.Add(map(owners));
                }
            }
            return list;
        }

        public OwnerDto map(Owner s)
        {
            OwnerDto owner = new OwnerDto();
            owner.ID = s.ID;
            owner.OwnerName = s.OwnerName;
            
            if (s.Properties != null)
            {
                foreach (var propertyDto in s.Properties)
                {
                    owner.Properties.Add(map(propertyDto));
                }
            }
            return owner;
        }

        public PropertyDto map(Property s)
        {
            PropertyDto property = new PropertyDto();
            property.ID = s.ID;
            property.OwnerID = s.OwnerID;
            property.PropertyName = s.PropertyName;
            property.OwnerName = s.OwnerName;

            property.Adress = s.Adress;
            property.ZIP = s.ZIP;
            property.SunRate = new SunRate();
            property.SunRate.January=s.SunRate.January;
            property.SunRate.February = s.SunRate.February;
            property.SunRate.Mars = s.SunRate.Mars;
            property.SunRate.April = s.SunRate.April;
            property.SunRate.May = s.SunRate.May;
            property.SunRate.Jun = s.SunRate.Jun;
            property.SunRate.July = s.SunRate.July;
            property.SunRate.August= s.SunRate.August;
            property.SunRate.September = s.SunRate.September;
            property.SunRate.October = s.SunRate.October;
            property.SunRate.November = s.SunRate.November;
            property.SunRate.December= s.SunRate.December;


            if (s.Vertices != null)
            {

                foreach (Map_Point item in s.Vertices)
                {
                    if (item != null) property.Vertices.Add(map(item));


                }

            }
            return property;
        }

        public MapPointDto map(Map_Point s)
        {
            MapPointDto map = new MapPointDto();
            map.X = s.X;
            map.Y = s.Y;
            map.GeoIndex = s.GeoIndex;
            return map;
        } 
        #endregion


    }
}
