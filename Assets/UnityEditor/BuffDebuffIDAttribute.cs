using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class BuffDebuffIDAttribute : Attribute
{
    public int ID { get; }

    public BuffDebuffIDAttribute(int id)
    {
        ID = id;
    }
}