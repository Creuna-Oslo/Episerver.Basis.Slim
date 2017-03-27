## Edit Mode features

Some extra features are included for edit mode.

### Enum translations
A selection factory, **EnumSelectionFactory<TEnum>**, is added that enables translations of an enum.
It can be found in the namespace [Your solution name].Web.Business.SelectionFactories

    [lang=CSharp]
    [SelectOne(SelectionFactoryType = typeof(EnumSelectionFactory<OpenGraphType>))]

The translations are fetched from lang/translations.[lang] using the xpath /Enums/[enumtype]/[stringvalue].


### Tab ordering and access control
An initializable module in App_Start called TabInitializer is used to initialize custom tabs with a specific sort index and access level.
To add a new tab, add the tab name to the ApplicationConstants.TabNames-class and add a new entry in the GetTabs-method.

    [lang=CSharp]
    yield return new TabDefinition
    {
        Name = ApplicationConstants.TabNames.NewTab,
        RequiredAccess = AccessLevel.Edit,
        SortIndex = 500
    };



### Max Content Area items

To limit the number of elements in a content area, add the attribute [MaxContentAreaItemCount(Limit = X)] to the property.
This can be found in the [Your solution name].Web.Business.Validation-namespace.

