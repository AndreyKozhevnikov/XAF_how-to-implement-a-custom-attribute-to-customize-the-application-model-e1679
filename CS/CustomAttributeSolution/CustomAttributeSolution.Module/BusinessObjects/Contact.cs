using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DXExample.Module;
using System.Collections.ObjectModel;

namespace dxTestSolution.Module.BusinessObjects;

[DefaultClassOptions]
public class Contact : BaseObject {
    public virtual string FirstName { get; set; }
    [RemoveFromViewModel]
    public virtual string LastName { get; set; }
    public virtual int Age { get; set; }
    public virtual DateTime BirthDate { get; set; }

    public virtual ObservableCollection<MyTask> MyTasks { get; set; } = new ObservableCollection<MyTask>();
}

   // public DbSet<MyTask> MyTasks { get; set; }
    // public DbSet<Contact> Contacts { get; set; }

