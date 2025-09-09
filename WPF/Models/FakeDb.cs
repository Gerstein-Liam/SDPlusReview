using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class FakeDb
    {
        public List<Owner> Proprietaires { get; set; }
        public FakeDb()
        {
            Proprietaires = new();

            Proprietaires.Add(new Owner() { ID = "Vin de Conthey" });
            Proprietaires.Last().Properties.Add(new Property()
            {
                ID = "Terrain CONTHEY-A",
                OwnerID = "Vin de Conthey",
                Vertices = new List<Map_Point> {
      new Map_Point( 812944.8541453549,5817631.331425033,0),
new Map_Point( 812932.0659509104,5817506.9772583665,0),
new Map_Point( 813067.0034509103,5817515.796702811,0),
new Map_Point( 813046.7187286881,5817632.213369478,0),
            }
            });
            Proprietaires.Last().Properties.Add(new Property()
            {
                ID = "Terrain CONTHEY-B",
                OwnerID = "Vin de Conthey",
                Vertices = new List<Map_Point> {


new Map_Point( 813088.6110897851,5817632.213369513,0),
new Map_Point( 813120.8020620072,5817521.970313958,0),
new Map_Point( 813261.0312286738,5817551.074480624,0),
new Map_Point( 813210.7603953405,5817664.845313958,0),
new Map_Point( 813130.5034508961,5817688.657813958,0),


                }
            });


            Proprietaires.Last().Properties.Add(new Property()
            {
                ID = "Terrain CONTHEY-C",
                OwnerID = "Vin de Conthey",
                Vertices = new List<Map_Point> {


new Map_Point( 813132.267339785,5817493.748091736,0),
new Map_Point( 813145.0555342294,5817464.64392507,0),
new Map_Point( 813188.2708120071,5817475.227258403,0),
new Map_Point( 813178.5694231183,5817510.505036181,0),


                }
            });


            //GROUP
            Proprietaires.Add(new Owner() { ID = "Vin de Sion" });
            Proprietaires.Last().Properties.Add(new Property()
            {
                ID = "Terrain SION-A",
                OwnerID = "Vin de Sion",
                Vertices = new List<Map_Point> {
new Map_Point( 812990.835722165,5817467.763169518,0),
new Map_Point( 812998.0739105127,5817452.342681298,0),
new Map_Point( 813012.0782314467,5817457.377942758,0),
new Map_Point( 813004.2106354164,5817473.270486739,0),
            }
            });

            Proprietaires.Last().Properties.Add(new Property()
            {
                ID = "Terrain SION-B",
                OwnerID = "Vin de Sion",
                Vertices = new List<Map_Point> {
new Map_Point( 813013.4204555525,5817477.781467506,0),
new Map_Point( 813017.9202288043,5817468.406939898,0),
new Map_Point( 813044.3563966583,5817481.156297444,0),
new Map_Point( 813048.0419440027,5817475.572877473,0),
new Map_Point( 813052.6541966621,5817478.319499843,0),
new Map_Point( 813049.4670782514,5817483.812744584,0),
new Map_Point( 813050.5553625867,5817484.512355942,0),
new Map_Point( 813043.2223622627,5817493.085059813,0),
new Map_Point( 813029.1989602378,5817488.0767019475,0),
new Map_Point( 813023.3710529028,5817481.338184091,0),
            }
            });
        }
    }
}
