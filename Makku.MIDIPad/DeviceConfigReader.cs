using System.Xml.Linq;
using Makku.MIDIPad.Core;

namespace Makku.MIDIPad;

public class DeviceConfigReader : BaseConfigReader, IConfigReader<MidiDevice>
{
    public MidiDevice ReadConfig(string configPath)
    {
        var xDocument = ReadXml(configPath);
        
        var root = xDocument.Root ?? throw Missing("Root element");
        CheckOrThrow(root.Name == "midiDevice", "Root element");
        
        var deviceId = root.Attribute("id")?.Value ?? throw Missing("Device Id");

        var names = ReadNames(root);
        var padTypes = ReadPadTypes(root);
        var pads = ReadPads(root, padTypes);

        return new MidiDevice(deviceId, names, pads);
    }
    
    private static string[] ReadNames(XElement rootNode)
    {
        var namesNode = rootNode.Element("names") ?? throw Missing("names");
        var names = namesNode
            .Descendants("name")
            .Select(nameElement => nameElement.Value ?? throw Missing("name value"))
            .ToArray();

        return names;
    }

    private static PadType[] ReadPadTypes(XElement rootNode)
    {
        var padTypesNode = rootNode.Element("padTypes") ?? throw Missing("padTypes");
        var padTypes = padTypesNode
            .Descendants("padType")
            .Select(padTypeElement =>
            {
                var padTypeName = padTypeElement.Attribute("name")?.Value ?? throw Missing("padType name");
                var padType = new PadType
                {
                    Name = padTypeName
                };

                return padType;
            })
            .ToArray();
        
        CheckOrThrow(padTypes.Length > 0, "padTypes");

        return padTypes;
    }

    private static Pad[] ReadPads(XElement rootNode, PadType[] padTypes)
    {
        var padsNode = rootNode.Element("pads") ?? throw Missing("pads");
        var pads = padsNode
            .Descendants("pad")
            .Select(padElement =>
            {
                var padId = padElement.Attribute("id")?.Value ?? throw Missing("pad id");
                
                var padTypeName = padElement.Attribute("type")?.Value ?? throw Missing("pad type");
                var padType = padTypes.FirstOrDefault(pt => pt.Name == padTypeName) ?? throw Missing("pad type");

                var pad = new Pad
                {
                    Id = padId,
                    Type = padType!
                };

                return pad;
            })
            .ToArray();

        var padGroupItems = padsNode
            .Descendants("group")
            .Select(padGroupElement =>
            {
                var groupPadTypeName = padGroupElement.Attribute("type")?.Value;
                
                var padsInGroup = padGroupElement
                    .Descendants("pad")
                    .Select(padElement =>
                    {
                        var padId = padElement.Attribute("id")?.Value ?? throw Missing("pad id");

                        var padTypeName = padElement.Attribute("type")?.Value ?? groupPadTypeName ?? throw Missing("pad type");
                        var padType = padTypes.FirstOrDefault(pt => pt.Name == padTypeName) ?? throw Missing("pad type");
                        
                        var pad = new Pad
                        {
                            Id = padId,
                            Type = padType!
                        };
                        return pad;
                    })
                    .ToArray();
                CheckOrThrow(padsInGroup.Length > 0, "pads in group");

                return padsInGroup;
            })
            .SelectMany(x => x);
        
        pads = pads.Concat(padGroupItems).ToArray();

        CheckOrThrow(pads.Length > 0, "pads");

        return pads;
    }
    
    private static void CheckOrThrow(bool value, string name)
    {
        if (!value)
        {
            throw new InvalidOperationException($"Invalid XML format: {name} is missing.");
        }
    }
    
    private static InvalidOperationException Missing(string name)
    {
        return new InvalidOperationException($"Invalid XML format: {name} is missing.");
    }
}

public class BaseConfigReader
{
    protected static XDocument ReadXml(string configPath)
    {
        if (!File.Exists(configPath))
        {
            throw new FileNotFoundException($"Config file not found: {configPath}");
        }

        return XDocument.Load(configPath);
    }
    
    protected T[] ReadGroup<T>()
}

public interface IConfigReader<out T>
{
    T ReadConfig(string configPath);
}