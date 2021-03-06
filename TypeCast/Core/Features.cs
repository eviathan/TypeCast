using System;
using System.Linq;
using System.Reflection;
using Umbraco.Core;

namespace TypeCast
{
    public sealed class Features
    {
        internal Features()
        {
            var props = this.GetType().GetProperties().Where(x => x.GetCustomAttribute<FeatureAttribute>() != null).ToDictionary(x => x, x => x.GetCustomAttribute<FeatureAttribute>(false));
            foreach (var prop in props)
            {
                prop.Key.SetValue(this, prop.Value.DefaultValue);
            }
        }

        /// <summary>
        /// <para>
        /// Affects the behaviour of the database interaction &amp; type synchronisation during initialisation.
        /// </para>
        /// <para> </para>
        /// <para>
        /// This is useful in load-balanced scenarios, where the developer may wish to nominate a "master" instance to
        /// handle updating the database whilst the rest of the instances are in "ensure" mode and only check that the database is correct before
        /// starting up or failing, depending on the outcome of the check.
        /// </para>
        /// <para> </para>
        /// <para>
        /// InitialisationMode.Sync - The types in the database will be updated to reflect the
        /// type definitions specified.
        /// </para>
        /// <para> </para>
        /// <para>
        /// InitialisationMode.Ensure - If the types in the database do not match the type definitions specified
        /// an exception will be thrown, and the database will not be modified.
        /// </para>
        /// <para> </para>
        /// <para>
        /// InitialisationMode.Passive - The type definitions are used as specified without checking if the database
        /// matches the definitions. This is useful in load balanced scenarios where one
        /// master instance will initialise the database whilst n slave instances will
        /// simply assume that the database will be synchronised to the type definitions
        /// before traffic is routed to them. It is the developer's responsibility to ensure
        /// that this happens. If passive instances are used before the database types are
        /// synchronised spurious behaviour may occur.
        /// </para>
        /// <para> </para>
        /// <para>
        /// Status: Stable (default: InitialisationMode.Sync)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = InitialisationMode.Sync)]
        public InitialisationMode InitialisationMode { get; set; }

        /// <summary>
        /// <para>
        /// Write output to the standard log file
        /// </para>
        /// <para>
        /// Status: Stable (default: false)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = false)]
        public bool WriteLogOutput { get; set; }

        /// <summary>
        /// <para>
        /// Hide any code-first-managed content types from the developer &amp; settings trees in the Umbraco back-office
        /// </para>
        /// <para>
        /// Status: Stable (default: false)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = false)]
        public bool HideCodeFirstEntityTypesInTrees { get; set; }

        /// <summary>
        /// <para>
        /// Register built-in converters and data type instances for string, int, DateTime and bool types
        /// </para>
        /// <para>
        /// Status: Stable (default: true)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = true)]
        public bool UseBuiltInPrimitiveDataTypes { get; set; }

        /// <summary>
        /// <para>
        /// Add all known code-first media types as allowed children of the default Folder media type
        /// </para>
        /// <para>
        /// Status: Stable (default: true) (does nothing if UseBuiltInMediaTypes is false)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = true)]
        public bool AllowAllMediaTypesInDefaultFolder { get; set; }

        /// <summary>
        /// <para>
        /// Use the built-in code-first classes for the default media types - Image, File and Folder (do this if you don't need to modify those types)
        /// </para>
        /// <para>
        /// Status: Stable (default: false) - this defaults to false as it can be very confusing when converting an existing site to code-first if this defaults to true
        /// </para>
        /// </summary>
        [Feature(DefaultValue = false)]
        public bool UseBuiltInMediaTypes { get; set; }

        /// <summary>
        /// <para>
        /// Use Castle DynamicProxy class proxies to enable lazy loading of 
        /// [ContentProperty] and [ContentComposition] properties, where the property is
        /// declared as virtual.
        /// </para>
        /// <para>
        /// Status: Stable (default: true)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = true)]
        public bool UseLazyLoadingProxies { get; set; }

        /// <summary>
        /// <para>
        /// Allow data types to access custom attributes from their model rendering ancestry (data type itself, current property, 
        /// current content type, current composing type, current composition property) when rendering. Currently used to allow
        /// CSS classes and data- attributes to be applied to emitted HTML elements, e.g. so all image elements have class="codefirst-image"
        /// or all emitted elements on a content type have data-mydata="myValue"
        /// </para>
        /// <para>
        /// HTML is emitted by any data type which implements IHtmlString, including many of the built-in ones, when the property is accessed
        /// within an HTML element body on a Razor view causing the Razor renderer to call ToHtmlString on the data type. 
        /// If the property is accessed within a HTML element tag (e.g. as an attribute value) then
        /// ToString() is called instead. In general the ToHtmlString method will render a sensible entire element (i.e. img element for an image, anchor element for a RelatedLink)
        /// where the ToString() method will render the most relevant bit of data (i.e. the image URL or the RelatedLink URL).
        /// </para>
        /// <para>
        /// Traversing the object tree to find attributes involves a lot of attribute lookups so the first load of each document type can be a bit heavier than usual, but after the first
        /// load the attributes will be cached so only a few dictionary look-ups are required and performance isn't too bad.
        /// </para>
        /// <para>
        /// Status: Experimental (default: false)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = false)]
        public bool UseContextualAttributes { get; set; }

        /// <summary>
        /// <para>
        /// Enables multi-threaded timers which measure the performance (in terms of wall-clock time) of the core modules during initialisation.
        /// You can write a verbose output of the measurements to a folder of your choice by calling Diagnostics.Timing.SaveReport(_filePath); after
        /// initialisation, but *before* turning this feature off.
        /// </para>
        /// <para>
        /// It is recommended that you turn this feature on immediately before intialisation and off immediately after (after saving the report of course).
        /// </para>
        /// <para>
        /// Status: Stable (default: false)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = false)]
        public bool EnablePerformanceDiagnosticTimer
        {
            get
            {
                return Diagnostics.Timing.Enabled;
            }
            set
            {
                Diagnostics.Timing.Enabled = value;
            }
        }

        /// <summary>
        /// <para>
        /// Enables raising of events to content models which implement 
		/// event interfaces or specify event handlers which do so.
        /// </para>
        /// <para>
        /// Status: Stable (default: true)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = true)]
        public bool EnableContentEvents { get; set; }

        /// <summary>
        /// <para>
        /// Use the built-in code-first classes for the default Umbraco data types
        /// </para>
        /// <para>
        /// Status: Stable (default: true)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = true)]
        public bool UseBuiltInUmbracoDataTypes { get; set; }

        /// <summary>
        /// <para>
        /// Use multiple threads to initialise the data/content types more quickly
        /// </para>
        /// <para>
        /// WARNING: This does not work when distributed calls are enabled and hence cannot
        /// be used in load-balanced scenarios.
		/// UPDATE: Works in 7.4.x for farm/balanced scenarios as distributed calls are not used.
        /// </para>
        /// <para>
        /// Status: Stable (default: true)
        /// </para>
        /// </summary>
        [Feature(DefaultValue = true)]
        public bool UseConcurrentInitialisation { get; set; }
    }

    internal sealed class FeatureAttribute : Attribute
    {
        public object DefaultValue { get; set; }
    }
}