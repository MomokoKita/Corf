using System;

[Serializable]
public class TextEventClass
{
    public Event[] event_one;
    public Event[] event_two;
    public Event[] event_three;
}

[Serializable]
public class Event
{
    public string name;
    public string main;
    public int[] face;
}
