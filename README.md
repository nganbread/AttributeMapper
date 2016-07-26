AttributeMapper
===============

A small C# library to orchestrate mappings using property and field attributes

How
===============

To map properties and fields on the following

    public class Source
    {
        public int Property { get; set; }
      
        public int Field;
      
        public int SourceProperty { get; set; }
      
        [MapTo(nameof(Destination.DestinationField))]
        public int SourceField;
      
        public Source Source;
    }
    
    public class Destination
    {
        public int Property { get; set; }
      
        public int Field;
      
        [MapFrom(nameof(Source.SourceProperty))]
        public int DestinationProperty { get; set;}
      
        public int DestinationField;
      
        [MapFrom(nameof(Source)]
        public Destination Destination;
    }
    
Is as easy as
    
    var source = new Source
    {
        Property = 1,
        Field = 2,
        SourceProperty = 3,
        SourceField = 4,
        Source = new Source
        {
            Property = 5,
            Field = 6,
            SourceProperty = 7,
            SourceField = 8,
            Source = null
        }
    };
    
    var destination = AttributeMapper.Map<Source, Destination>(source);
    
    // destination.Property                      -> 1
    // destination.Field                         -> 2
    // destination.DestinationProperty           -> 3
    // destination.DestinationField              -> 4
    // destination.Destination.Property          -> 5
    // destination.Destination.Field             -> 6
    // destination.Destination.SourceProperty    -> 7
    // destination.Destination.SourceField       -> 8
    // destination.Destination.Destination       -> null
