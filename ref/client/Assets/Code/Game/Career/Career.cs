using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fogs.proto.msg;
using fogs.proto.config;


public class Section
{
    public Section() { }

    public uint id;
    public bool is_complete = false;
    public uint challenge_times = 0;
    public uint buy_times = 0;
    public uint medal_rank = 0;
    public uint player_id;
    public uint get_role;
    public uint is_activate;

    public void Init(SectionProto data, uint playerID)
    {
        id = data.id;
        is_complete = data.is_complete == 1 ? true : false;
        challenge_times = data.challenge_times;
        buy_times = data.buy_times;
        medal_rank = data.medal_rank;
        player_id = playerID;
        get_role = data.get_role;
        is_activate = data.is_activate;
    }

    public void ChangeData(SectionProto data)
    {
        id = data.id;
        is_complete = data.is_complete == 1 ? true : false;
        challenge_times = data.challenge_times;
        buy_times = data.buy_times;
        medal_rank = data.medal_rank;
    }
};

public class Chapter
{
    public Chapter() { }

    public uint id;
    public bool is_complete = false;
    public bool is_bronze_awarded = false;
    public bool is_silver_awarded = false;
    public bool is_gold_awarded = false;
    public uint star_num = 0;
	public uint player_id;
    public Dictionary<uint, Section> sections = new Dictionary<uint, Section>();

    public void Init(ChapterProto data, uint playerID)
    {
        id = data.id;
        is_complete = data.is_complete == 1 ? true : false;
        is_bronze_awarded = data.is_bronze_awarded == 1 ? true : false;
        is_silver_awarded = data.is_silver_awarded == 1 ? true : false;
        is_gold_awarded = data.is_gold_awarded == 1 ? true : false;
        star_num = data.star_num;
        player_id = playerID;
        for (int i = 0; i < data.sections.Count; ++i)
        {
            uint secitonID = data.sections[i].id;
            if (sections.ContainsKey(secitonID) == false)
            {
                Section section = new Section();
                section.Init(data.sections[i], playerID);
                sections[secitonID] = section;
            }
        }
    }

    public void ChangeData(ChapterProto data)
    {
        is_complete = data.is_complete == 1 ? true : false;
        is_bronze_awarded = data.is_bronze_awarded == 1 ? true : false;
        is_silver_awarded = data.is_silver_awarded == 1 ? true : false;
        is_gold_awarded = data.is_gold_awarded == 1 ? true : false;
        star_num = data.star_num;
        for (int i = 0; i < data.sections.Count; ++i)
        {
            uint sectionID = data.sections[i].id;
            if (sections.ContainsKey(sectionID) == false)
            {
                Section section = new Section();
                section.Init(data.sections[i], player_id);
                sections[sectionID] = section;
            }
            else
            {
                sections[sectionID].ChangeData(data.sections[i]);
            }
        }
    }

    public bool CheckSectionComplete(uint sectionID)
    {
        if (sections.ContainsKey(sectionID))
        {
            return sections[sectionID].is_complete;
        }
        return false;
    }

    public bool CheckAllSectionComplete()
    {
        foreach (KeyValuePair<uint, Section> iter in sections)
        {
            if (iter.Value.is_complete == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool AddFirstSection()
    {
        ChapterConfig data = GameSystem.Instance.CareerConfigData.GetChapterData(id);
        if (data != null)
        {
            AddSection(data.first_section_id);
            return true;
        }
        return false;
    }

    public bool AddSection(uint sectionID)
    {
        if (sections.ContainsKey(sectionID))
        {
            return false;
        }
        Section section = new Section();
        section.id = sectionID;
        sections[sectionID] = section;
        return true;
    }

};
