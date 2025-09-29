using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class AppData
    {
        public List<Owner> OwnersList { get; set; } = new List<Owner>();
        public void AddOwner(Owner owner)
        {
            OwnersList.Add(owner);
        }


        public void updateOwner(string ownerID,Owner owner)
        {
            Owner? Target = OwnersList.Where(x => x.ID == ownerID).FirstOrDefault();
            if (Target!=null)
            {
                Target.OwnerName=owner.OwnerName;
            }
        }

        public void DeleteOwner(string ownerID)
        {
            Owner? Target = OwnersList.Where(x => x.ID == ownerID).FirstOrDefault();
            if (Target != null)
            {
                OwnersList.Remove(Target);  
            }
        }




        public void TransferProperty(string currentOwnerID, string targetOwnerID, string propertyID) {

            Owner? targetOwner = OwnersList.Where(x => x.ID == targetOwnerID).FirstOrDefault();
            Owner? currentOwner = OwnersList.Where(x => x.ID == currentOwnerID).FirstOrDefault();
            if (currentOwner != null && targetOwner!=null) {

                Property? p = currentOwner.Properties.Where(x => x.ID == propertyID).FirstOrDefault();
                if (p != null) { 
                p.OwnerID= targetOwnerID;   
                targetOwner.Properties.Add(p);
                currentOwner.Properties.Remove(p);
                }
            }

           
        
        }

        public void AddProperty(Owner owner, Property property)
        {
            
            property.OwnerName = owner.OwnerName;   
            owner.Properties.Add(property);
        }
        public void AddPropertyUsingOwnerID(string ownerID, Property property)
        {
            property.PrintMe();
            Owner? Target = OwnersList.Where(x => x.ID == ownerID).FirstOrDefault();
            if (Target != null)
            {
                AddProperty(Target, property);
            }
        }

        public Property GetProperty(string ownerID, string propertyID) {

            Owner? owner = OwnersList.Where(x => x.ID == ownerID).FirstOrDefault();
            if (owner != null)
            {
                Property? property = owner.Properties.Where(x => x.ID == propertyID).FirstOrDefault();
                return property;
            }
            return null;

        }

        public void RemoveProperty(Owner owner, Property property)
        {
            owner.Properties.Remove(property);
        }
        public void RemovePropertyUsingCloneAsEntry(Owner owner, Property property)
        {
            Property? original = owner.Properties.Where(x => x.ID == property.ID).FirstOrDefault();
            if (original != null) RemoveProperty(owner, original!);
        }


        public void RemovePropertyUsingOwnerID(string ownerID, Property property)
        {

            Debug.WriteLine($"owner ID{ownerID} : propertyOwnerId{property.OwnerID}");

            Owner? Owner = OwnersList.Where(x => x.ID == ownerID).FirstOrDefault();
            if (Owner != null)
            {
                RemovePropertyUsingCloneAsEntry(Owner, property);
            }
        }


        public void UpdatePropertyUsingOwnerID(string ownerID, Property property)
        {
            Owner? Target = OwnersList.Where(x => x.ID == ownerID).FirstOrDefault();
            if (Target != null)
            {
                Property? t = Target.Properties.Where(x => x.ID == property.ID).FirstOrDefault();
                if (t != null) UpdatePropertyData(t, property);
            }
        }
        private void UpdatePropertyData(Property Target, Property Update)
        {
            Target.Vertices = Update.Vertices;
            Target.Area = Update.Area;
            Target.Center = Update.Center;
        }
    }
}
