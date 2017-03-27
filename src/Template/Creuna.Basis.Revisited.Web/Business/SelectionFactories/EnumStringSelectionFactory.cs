using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;

namespace Creuna.Basis.Revisited.Web.Business.SelectionFactories
{
    /// <summary>
    /// Implements EPiServer selection factory 
    /// where value is an enum item
    /// and a display name is a translation
    /// </summary>
    public class EnumSelectionFactory<TEnum> : ISelectionFactory
    {
        LocalizationService LocalizationService { get; }

        Type EnumType { get; }
        bool EnumIsNullable { get; }

        public EnumSelectionFactory()
            : this(ServiceLocator.Current.GetInstance<LocalizationService>())
        {
        }

        public EnumSelectionFactory(LocalizationService localizationService)
        {
            LocalizationService = localizationService;

            if (Nullable.GetUnderlyingType(typeof(TEnum)) != null)
            {
                EnumType = Nullable.GetUnderlyingType(typeof(TEnum));
                EnumIsNullable = true;
            }
            else
            {
                EnumType = typeof(TEnum);
            }
        }

        public virtual IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var items = Enum.GetValues(EnumType)
                .Cast<Enum>()
                .Select(item => new SelectItem
                {
                    Value = GetEnumItemValue(item),
                    Text = LocalizationService.GetString(GetEnumItemTranslationKey(item))
                })
                .ToList();

            if (EnumIsNullable)
            {
                items.Insert(0, new SelectItem
                {
                    Text = GetNullValueDisplayName()
                });
            }

            return items;
        }

        object GetEnumItemValue(Enum item)
           => item;

        string GetNullValueDisplayName()
            => string.Empty;

        protected virtual string GetEnumItemTranslationKey(Enum item)
        {
            var result = item.ToString().ToLowerInvariant();
            return $"/Enums/{typeof(TEnum).Name}/{result}";
        }
    }
}