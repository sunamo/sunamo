using System;
public interface IStatusBroadcaster
{
    event VoidObjectParamsObjects NewStatus;
    void OnNewStatus(string s, params object[] p);
}

/// <summary>
/// Dědí zároveň rozhraní IStatusBroadcaster, jen k němu přidává metodu a událost pro přidání ke stávajícímu obsahu statusu
/// </summary>
public interface IStatusBroadcasterAppend : IStatusBroadcaster
{
    event VoidObjectParamsObjects NewStatusAppend;
    void OnNewStatusAppend(string s, params object[] p);
}
