AttributeMapper
===============

A small mapping library to orchestrate maps by using property attributes

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

    var destination = AttributeMapper.Map<Source, Destination>(source);
