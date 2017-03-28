## Edit Mode features

Some extra features are included for edit mode.

### Enum translations
A selection factory, **EnumSelectionFactory<TEnum>**, is added that enables translations of an enum.
It can be found in the namespace [Your solution name].Web.Business.SelectionFactories

```csharp
[SelectOne(SelectionFactoryType = typeof(EnumSelectionFactory<OpenGraphType>))]
```

The translations are fetched from lang/translations.[lang] using the xpath /Enums/[enumtype]/[stringvalue].


### Tab ordering and access control
To add a new tab, add the tab name to the TabNames-class. An attribute to determine order and required access should be added.

```csharp    
[Display(Name = "SEO", Order = 1000)]
[RequiredAccess(AccessLevel.Edit)]
public const string Seo = "SEO";
```



### Max Content Area items

To limit the number of elements in a content area, add the attribute [MaxContentAreaItemCount(Limit = X)] to the property.
This can be found in the [Your solution name].Web.Business.Validation-namespace.

