

using DOMAIN;

namespace SDPlusApplicationServer_FakeDatabase.FakeDb
{
    public class FakeDb
    {


        public List<OwnerDto> Owners { get; }

        public FakeDb() {




            Owners= new List<OwnerDto>();
            




            List<MapPointDto> JoliAppartementGeometrie = new List<MapPointDto>() {


                  new MapPointDto() {X=812944.8541453549,Y=5817631.331425033,GeoIndex=0},
      new MapPointDto() {X=812932.0659509104,Y=5817506.9772583665,GeoIndex=1},
      new MapPointDto() {X=813067.0034509103,Y=5817515.796702811,GeoIndex=2},
      new MapPointDto() {X=813046.7187286881,Y=5817632.213369478,GeoIndex=3},


            };



            List<MapPointDto> JoliBunkerGeometrie = new List<MapPointDto>() {


          new MapPointDto() {X=813132.267339785,Y=5817493.748091736,GeoIndex=0},
      new MapPointDto() {X=813145.0555342294,Y=5817464.64392507,GeoIndex=1},
      new MapPointDto() {X=813188.2708120071,Y=5817475.227258403,GeoIndex=2},
      new MapPointDto() {X=813178.5694231183,Y=5817510.505036181,GeoIndex=3}


            };

            List<MapPointDto> JoliCartonGeometrie = new List<MapPointDto>() {


      new MapPointDto() {X=812990.835722165,Y=5817467.763169518,GeoIndex=0},
      new MapPointDto() {X=812998.0739105127,Y=5817452.342681298,GeoIndex=1},
      new MapPointDto() {X=813012.0782314467,Y=5817457.377942758,GeoIndex=2},
      new MapPointDto() {X=813004.2106354164,Y=5817473.270486739,GeoIndex=3},


            };


            OwnerDto ImmoSchmitz = new OwnerDto("ImmoSchmitz", new List<PropertyDto>() );
            OwnerDto LaCoopMan = new OwnerDto("La cooperative de la Rue", new List<PropertyDto>() );

            PropertyDto JoliAppartement= new PropertyDto("Joli Appartement", "ImmoSchmitz", JoliAppartementGeometrie) {OwnerID=ImmoSchmitz.ID };
            PropertyDto JoliBunker = new PropertyDto("JoliBunker", "ImmoSchmitz", JoliBunkerGeometrie) { OwnerID = ImmoSchmitz.ID };
            PropertyDto JoliCarton = new PropertyDto("Joli Appartement", "La cooperative de la Rue", JoliCartonGeometrie) { OwnerID = LaCoopMan.ID };

            ImmoSchmitz.Properties.Add(JoliAppartement);
            ImmoSchmitz.Properties.Add(JoliBunker);
            LaCoopMan.Properties.Add(JoliCarton);




            Owners.Add(ImmoSchmitz);
            Owners.Add(LaCoopMan);
            





        }
    }
}
