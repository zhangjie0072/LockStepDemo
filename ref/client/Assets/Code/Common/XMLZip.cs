using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Text;

internal enum ItemType
{
    Element,
    Attritube
}
internal class XmlNodeItem
{
    public string Xpath { get; set; }
    public string Text { get; set; }
    public ItemType ItemType { get; set; }
    public override string ToString()
    {
        return Xpath;
    }
}
internal class MyXpath
{
    LinkedList<string> _node = new LinkedList<string>();
    public void AddElement(string name)
    {
        _node.AddLast(string.Format("/{0}", name));
    }
    public void AddAttribute(string name)
    {
        _node.AddLast(string.Format("/@{0}", name));
    }
    public void RemoveLastElement()
    {
        _node.RemoveLast();
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        LinkedListNode<string> node = _node.First;
        sb.Append(node.Value);
        while ((node = node.Next) != null)
        {
            sb.Append(node.Value);
        }
        return sb.ToString();
    }
}
class XmlZip
{
    Dictionary<ushort, XmlNodeItem> _map = new Dictionary<ushort, XmlNodeItem>();
    Dictionary<string, ushort> _map2 = new Dictionary<string, ushort>();
    MyXpath _path = new MyXpath();

    public void Init(string xmlInput)
    {
        StringReader sr = new StringReader(xmlInput);
        XmlReader reader = XmlReader.Create(sr);
        MemoryStream ms = new MemoryStream();
        ushort i = 1;
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    _path.AddElement(reader.Name);
                    _map[i++] = new XmlNodeItem()
                    {
                        Xpath = _path.ToString(),
                        Text = reader.Name,
                        ItemType = ItemType.Element
                    };
                    if (reader.HasAttributes)
                    {
                        reader.MoveToFirstAttribute();
                        _path.AddAttribute(reader.Name);
                        _map[i++] = new XmlNodeItem()
                        {
                            Xpath = _path.ToString(),
                            Text = reader.Name,
                            ItemType = ItemType.Attritube
                        };
                        _path.RemoveLastElement();
                        while (reader.MoveToNextAttribute())
                        {
                            _path.AddAttribute(reader.Name);
                            _map[i++] = new XmlNodeItem()
                            {
                                Xpath = _path.ToString(),
                                Text = reader.Name,
                                ItemType = ItemType.Attritube
                            };
                            _path.RemoveLastElement();
                        }
                        reader.MoveToElement();
                    }
                    if (reader.IsEmptyElement) _path.RemoveLastElement();
                    break;
                case XmlNodeType.EndElement:
                    _path.RemoveLastElement();
                    break;
                default:
                    break;
            }
        }
        foreach (KeyValuePair<ushort, XmlNodeItem> pair in _map)
        {
            _map2[pair.Value.Xpath] = pair.Key;
        }
    }

    private void SerializeMap(BinaryWriter bw)
    {
        bw.Write(_map.Count);
        //byte[] bs;
        foreach (KeyValuePair<ushort, XmlNodeItem> pair in _map)
        {
            bw.Write(pair.Key);
            //bs = Encoding.UTF8.GetBytes(pair.Value.Xpath);
            //bw.Write(bs.Length);
            //bw.Write(bs);
            //bs = Encoding.UTF8.GetBytes(pair.Value.Text);
            //bw.Write(bs.Length);
            //bw.Write(bs);
            bw.Write(pair.Value.Xpath);
            bw.Write(pair.Value.Text);
            bw.Write((int)pair.Value.ItemType);
        }
    }

    private void UnserializeMap(BinaryReader br)
    {
        int count = br.ReadInt32();
        for (int i = 0; i < count; ++i)
        {
            ushort key = (ushort)br.ReadInt16();
            XmlNodeItem item = new XmlNodeItem();
            item.Xpath = br.ReadString();
            item.Text = br.ReadString();
            item.ItemType = (ItemType)br.ReadInt32();
            _map[key] = item;
        }
    }

    public byte[] XmlToBytes(string xmlInput)
    {
        StringReader sr = new StringReader(xmlInput);
        XmlReader reader = XmlReader.Create(sr);
        MemoryStream ms = new MemoryStream();
        BinaryWriter bw = new BinaryWriter(ms);
        SerializeMap(bw);
        while (reader.Read())
        {
            ushort index;
            byte[] bs;
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    _path.AddElement(reader.Name);
                    if (_map2.TryGetValue(_path.ToString(), out index))
                    {
                        bw.Write(index);
                    }
                    if (reader.HasAttributes)
                    {
                        reader.MoveToFirstAttribute();
                        _path.AddAttribute(reader.Name);
                        if (_map2.TryGetValue(_path.ToString(), out index))
                        {
                            _path.RemoveLastElement();
                            bw.Write(index);
                            bs = Encoding.UTF8.GetBytes(reader.Value);
                            bw.Write((ushort)bs.Length);
                            bw.Write(bs);
                        }
                        while (reader.MoveToNextAttribute())
                        {
                            _path.AddAttribute(reader.Name);
                            if (_map2.TryGetValue(_path.ToString(), out index))
                            {
                                _path.RemoveLastElement();
                                bw.Write(index);
                                bs = Encoding.UTF8.GetBytes(reader.Value);
                                bw.Write((ushort)bs.Length);
                                bw.Write(bs);
                            }
                        }
                        reader.MoveToElement();
                    }
                    if (reader.IsEmptyElement)
                    {
                        _path.RemoveLastElement();
                        bw.Write(ushort.MaxValue);
                    }
                    break;
                case XmlNodeType.EndElement:
                    _path.RemoveLastElement();
                    bw.Write(ushort.MaxValue);
                    break;
                case XmlNodeType.Text:
                    bw.Write((ushort)0);
                    bs = Encoding.UTF8.GetBytes(reader.Value);
                    bw.Write((ushort)bs.Length);
                    bw.Write(bs);
                    break;
                default:
                    break;
            }
        }
        bw.Close();
        ms.Close();
        reader.Close();
        return ms.ToArray();
    }

    public string BytesToXml(byte[] bytes)
    {
        MemoryStream ms = new MemoryStream(bytes);
        BinaryReader br = new BinaryReader(ms);
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        XmlWriter writer = XmlWriter.Create(sw, settings);

        string strRet = string.Empty;
        UnserializeMap(br);
        XmlNodeItem item;
        while (br.PeekChar() != -1)
        {
            ushort readFlag = br.ReadUInt16();
            int len;
            byte[] bs;
            string str;
            if (_map.TryGetValue(readFlag, out item))
            {
                if (item.ItemType == ItemType.Element)
                {
                    writer.WriteStartElement(item.Text);
                }
                else if (item.ItemType == ItemType.Attritube)
                {
                    len = br.ReadUInt16();
                    bs = br.ReadBytes(len);
                    str = Encoding.UTF8.GetString(bs);
                    writer.WriteAttributeString(item.Text, str);
                }
            }
            else if (readFlag == 0)
            {
                len = br.ReadUInt16();
                bs = br.ReadBytes(len);
                str = Encoding.UTF8.GetString(bs);
                writer.WriteString(str);
            }
            else if (readFlag == ushort.MaxValue)
            {
                writer.WriteEndElement();
            }
        }
        writer.Flush();
        writer.Close();
        sw.Close();
        br.Close();
        return strRet;
    }

    //public XmlReader BytesToXmlTest(byte[] bytes)
    //{
    //    MemoryStream ms = new MemoryStream(bytes);
    //    BinaryReader br = new BinaryReader(ms);
    //    XmlWriterSettings settings = new XmlWriterSettings();
    //    settings.Indent = true;
    //    XmlWriter writer = XmlWriter.Create(ms, settings);

    //    string strRet = string.Empty;
    //    UnserializeMap(br);
    //    XmlNodeItem item;
    //    while (br.PeekChar() != -1)
    //    {
    //        ushort readFlag = br.ReadUInt16();
    //        int len;
    //        byte[] bs;
    //        string str;
    //        if (_map.TryGetValue(readFlag, out item))
    //        {
    //            if (item.ItemType == ItemType.Element)
    //            {
    //                writer.WriteStartElement(item.Text);
    //            }
    //            else if (item.ItemType == ItemType.Attritube)
    //            {
    //                len = br.ReadUInt16();
    //                bs = br.ReadBytes(len);
    //                str = Encoding.UTF8.GetString(bs);
    //                writer.WriteAttributeString(item.Text, str);
    //            }
    //        }
    //        else if (readFlag == 0)
    //        {
    //            len = br.ReadUInt16();
    //            bs = br.ReadBytes(len);
    //            str = Encoding.UTF8.GetString(bs);
    //            writer.WriteString(str);
    //        }
    //        else if (readFlag == ushort.MaxValue)
    //        {
    //            writer.WriteEndElement();
    //        }
    //    }
    //    //writer.Flush();
    //    //writer.Close();
    //    br.Close();
    //    return ms;
    //}
}