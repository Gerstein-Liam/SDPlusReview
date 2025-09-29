using DOMAIN;

using System.Reflection.Metadata.Ecma335;

namespace SDPlusApplicationServer_FakeDatabase.FakeDb
{
    public class FakeDbController
    {


        private readonly FakeDb _fakeDb;


        public FakeDbController(FakeDb fakeDb)
        {
            _fakeDb = fakeDb;

        }

        private void CreateUniqueIdentifierForProperty(string gid,PropertyDto p)
        {

            p.ID = Guid.NewGuid().ToString();
            p.OwnerID = gid;
            foreach (var v in p.Vertices)
            {
                v.ID = Guid.NewGuid().ToString();
            }

        }

        private void CreateUniqueIdentifierForOwner(OwnerDto item)
        {
            item.ID = Guid.NewGuid().ToString();

            foreach (var p in item.Properties)
            {
                CreateUniqueIdentifierForProperty(item.ID, p);
            }
        }

        #region Owner



        public List<OwnerDto> GetAll()
        {

            Thread.Sleep(500);
            return _fakeDb.Owners;
        }
        public bool CreateOwner(OwnerDto Item)
        {
            CreateUniqueIdentifierForOwner(Item);
            _fakeDb.Owners.Add(Item);
            return true;
        }


        public bool CreateAllOwner(List<OwnerDto> Item)
        {
            _fakeDb.Owners.Clear();
            foreach (var item in Item)
            {
                CreateUniqueIdentifierForOwner(item);
                _fakeDb.Owners.Add(item);
            }


            return true;
        }


        public bool DeleteOwner(string id)
        {
            OwnerDto? RealInstance = _fakeDb.Owners.Where(o => o.ID == id).FirstOrDefault();
            if (RealInstance == null) return false;
            _fakeDb.Owners.Remove(RealInstance);
            return true;
        }

        public bool UpdateOwner(OwnerDto Item)
        {
            OwnerDto? RealInstance = _fakeDb.Owners.Where(o => o.ID == Item.ID).FirstOrDefault();
            if (RealInstance == null) return false;
            _fakeDb.Owners.Remove(RealInstance);
            _fakeDb.Owners.Add(Item);
            return true;
        }
        #endregion

        #region Property
        public bool CreateProperty(PropertyDto newProperty)
        {
            OwnerDto? TargetOwner = _fakeDb.Owners.Where(o => o.ID == newProperty.OwnerName).FirstOrDefault();
            if (TargetOwner == null) return false;
            newProperty.ID = Guid.NewGuid().ToString();
            TargetOwner.Properties.Add(newProperty);
            return true;
        }

        public bool DeleteProperty(PropertyDto PropertyToDelete)
        {
            OwnerDto? TargetOwner = _fakeDb.Owners.Where(o => o.ID == PropertyToDelete.OwnerName).FirstOrDefault();
            if (TargetOwner == null) return false;
            TargetOwner.Properties.Remove(PropertyToDelete);
            return true;
        }

        public bool UpdateProperty(PropertyDto Item)
        {
            OwnerDto? TargetOwner = _fakeDb.Owners.Where(o => o.OwnerName == Item.OwnerName).FirstOrDefault();
            if (TargetOwner == null ) return false;
            PropertyDto? propertyDtoToReplace = TargetOwner.Properties.Where(p => p.ID == Item.ID).FirstOrDefault();
            if ( propertyDtoToReplace == null) return false;
            
            TargetOwner.Properties.Remove(propertyDtoToReplace);
            TargetOwner.Properties.Add(Item);
            return true;
        }
        #endregion
    }
}
