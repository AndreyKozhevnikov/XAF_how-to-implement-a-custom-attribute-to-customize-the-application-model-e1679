<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128590989/23.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1679)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# How to implement a custom attribute to customize the Application Model

This example demonstrates how to create a custom attribute and remove a property from the [Application Model](https://docs.devexpress.com/eXpressAppFramework/112579/ui-construction/application-model-ui-settings-storage)'s DetailView.Items and ListView.Columns node.
In this case, the properties marked with this attribute will be removed from views.

## Implementation Details

To implement this functionality, the <strong>IModelMember</strong> interface is extended with the <strong>IRemovedFromViewInfo</strong> interface that contains a single property - <strong>IsRemovedFromViewInfo</strong>. The value of this property is calculated based on the value of the custom attribute we created (<strong>RemoveFromViewInfoAttribute</strong>) and applied to the persistent class. This is done via the domain logic (<strong>RemovedFromViewInfoLogic</strong>). Then the IRemovedFromViewInfo.IsRemovedFromViewInfo property is used by the model nodes generator updater (<strong>ViewsNodesGeneratorUpdater</strong>) to remove certain view items and columns from detail and list views. Thus, the example demonstrates three techniques: extending the Application Model with additional properties, defining default model values via the domain logic and changing the default model via the model nodes generator updater. 

## Files to Review

- [Contact.cs](CS/CustomAttributeSolution/CustomAttributeSolution.Module/BusinessObjects/Contact.cs)
- **[Module.cs](CS/CustomAttributeSolution/CustomAttributeSolution.Module/Module.cs)**
- [RemoveFromViewInfoAttribute.cs](CS/CustomAttributeSolution/CustomAttributeSolution.Module/RemoveFromViewInfoAttribute.cs)


## Documentation 

[Create Additional ListView Nodes in Code Using a Generator Updater](https://docs.devexpress.com/eXpressAppFramework/113315/ui-construction/application-model-ui-settings-storage/customize-application-model-in-code/create-additional-list-view-nodes-in-code-using-a-generator-updater)
[Add Custom Nodes and Properties to the Application Model in Code](https://docs.devexpress.com/eXpressAppFramework/404125/ui-construction/application-model-ui-settings-storage/modify-application-model/add-nodes-and-node-properties-to-app-model)
