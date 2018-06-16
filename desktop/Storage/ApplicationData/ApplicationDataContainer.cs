public class ApplicationDataContainer
{
    /// <summary>
    /// Don't use AppData Roaming or Local by default. therefore full path for file A1
    /// Inicializuji veškeré hodnoty, abych je pak už mohl pouze brát z paměti a nikoliv pořád načítat z disku
    /// </summary>
    /// <param name="path"></param>
    public ApplicationDataContainer(string path)
    {
        Values = new ApplicationDataContainerList(path);
    }

    public ApplicationDataContainerList Values = null;
}
