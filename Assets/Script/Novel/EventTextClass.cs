using System;

[Serializable]
public class TextEventClass
{
    public Event[] event_one;
}

[Serializable]
public class Event
{
    public string name;
    public string main;
}
