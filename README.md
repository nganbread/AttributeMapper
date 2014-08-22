AttributeMapper
===============

A small mapping library to orchestrate maps by using property and field attributes

How
===============

Mapping properties and fields on the following

    public class Source
    {
      public object Property { get; set; }
      public object Field;
      public object SourceProperty { get; set; }
      [MapTo("DestinationField")]
      public object SourceField;
    }
    
    public class Destination
    {
      public object Property { get; set; }
      public object Field;
      [MapFrom("SourceProperty")]
      public object DestinationProperty { get; set;}
      public object DestinationField;
    }
    
Is as easy as
    
    var source = new Source
    {
        Property = 1,
        Field = 2,
        SourceProperty = 3,
        SourceField = 4
    };
    
    var destination = AttributeMapper.Map<Source, Destination>(source);
    
    //destination.Property -> 1
    //destination.Field -> 2
    //destination.SourceProperty -> 3
    //destination.SourceField -> 4
