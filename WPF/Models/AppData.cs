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
        public List<Owner> Owners { get; set; } = new List<Owner>();
        public void AddProprietry(Owner newProprietaire)
        {
            Owners.Add(newProprietaire);
        }
        public void AddExploitation(Owner Proprietaire, Property exploitationItem)
        {
            Proprietaire.Properties.Add(exploitationItem);
        }
        public void AddExploitationUsingID(string ProprietryID, Property exploitationItem)
        {
            exploitationItem.PrintMe();
            Owner? Target = Owners.Where(x => x.ID == ProprietryID).FirstOrDefault();
            if (Target != null)
            {
                AddExploitation(Target, exploitationItem);
            }
        }
        public void RemoveExploitation(Owner proprietary, Property exploitationItem)
        {
            proprietary.Properties.Remove(exploitationItem);
        }
        public void RemoveExploitationCloneAsEntry(Owner owner, Property property)
        {
            Property? original = owner.Properties.Where(x => x.ID == property.ID).FirstOrDefault();
            if (RemoveExploitation != null) RemoveExploitation(owner, original!);
        }


        public void RemoveExploitationUsingID(string ownerID, Property Property)
        {

            Debug.WriteLine($"owner ID{ownerID} : propertyOwnerId{Property.OwnerID}");

            Owner? Owner = Owners.Where(x => x.ID == ownerID).FirstOrDefault();
            if (Owner != null)
            {
                RemoveExploitationCloneAsEntry(Owner, Property);
            }
        }


        public void UpdateExploitationUsingID(string ProprietryID, Property exploitationItem)
        {
            Owner? Target = Owners.Where(x => x.ID == ProprietryID).FirstOrDefault();
            if (Target != null)
            {
                Property? t = Target.Properties.Where(x => x.ID == exploitationItem.ID).FirstOrDefault();
                if (t != null) ReplaceContent(t, exploitationItem);
            }
        }
        private void ReplaceContent(Property Target, Property Update)
        {
            Target.Vertices = Update.Vertices;
            Target.Area = Update.Area;
            Target.Center = Update.Center;
        }
    }
}
