using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using dxTestSolution.Module.DatabaseUpdate;
using DXExample.Module;
using System.Collections;

namespace CustomAttributeSolution.Module;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class CustomAttributeSolutionModule : ModuleBase {
    public CustomAttributeSolutionModule() {
		// 
		// CustomAttributeSolutionModule
		// 
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
        ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
        return new ModuleUpdater[] { updater, new MyUpdater(objectSpace, versionFromDB) };
    }
    public override void Setup(XafApplication application) {
        base.Setup(application);
        // Manage various aspects of the application UI and behavior at the module level.
    }


    public override void ExtendModelInterfaces(DevExpress.ExpressApp.Model.ModelInterfaceExtenders extenders) {
        base.ExtendModelInterfaces(extenders);
        extenders.Add<IModelMember, IRemovedFromViewModel>();
    }
    public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters) {
        base.AddGeneratorUpdaters(updaters);
        updaters.Add(new ViewsNodesGeneratorUpdater());
    }
    public override void CustomizeLogics(CustomLogics customLogics) {
        base.CustomizeLogics(customLogics);
        customLogics.RegisterLogic(typeof(IRemovedFromViewModel), typeof(RemovedFromViewInfoLogic));
    }
}
[DomainLogic(typeof(RemovedFromViewInfoLogic))]
public interface IRemovedFromViewModel {
    bool IsRemovedFromViewModel { get; }
}
public class RemovedFromViewInfoLogic {
    public static bool Get_IsRemovedFromViewModel(IRemovedFromViewModel instance) {
        RemoveFromViewModelAttribute attr = ((IModelMember)instance).MemberInfo.FindAttribute<RemoveFromViewModelAttribute>();
        if (attr != null) {
            return attr.IsPropertyRemoved;
        } else {
            return false;
        }
    }
}
public class ViewsNodesGeneratorUpdater : ModelNodesGeneratorUpdater<ModelViewsNodesGenerator> {
    public override void UpdateNode(ModelNode node) {
        foreach (IModelView view in (IModelViews)node) {
            ArrayList itemsToRemove = new ArrayList();
            if (view is IModelDetailView) {
                foreach (IModelViewItem item in ((IModelDetailView)view).Items) {
                    if (item is IModelMemberViewItem) {
                        IRemovedFromViewModel member = ((IModelMemberViewItem)item).ModelMember as IRemovedFromViewModel;
                        if (member != null && member.IsRemovedFromViewModel) {
                            itemsToRemove.Add(item);
                        }
                    }
                }
            }
            if (view is IModelListView) {
                foreach (IModelColumn column in ((IModelListView)view).Columns) {
                    IRemovedFromViewModel member = column.ModelMember as IRemovedFromViewModel;
                    if (member != null && member.IsRemovedFromViewModel) {
                        itemsToRemove.Add(column);
                    }
                }
            }
            foreach (IModelNode item in itemsToRemove) {
                item.Remove();
            }
            if (view is IModelDetailView && itemsToRemove.Count > 0) {
                IModelViewLayout layoutModel = ((IModelDetailView)view).Layout;
                layoutModel[0].Remove();
                new ModelDetailViewLayoutNodesGenerator().GenerateNodes((ModelNode)layoutModel);
            }
        }
    }
}
