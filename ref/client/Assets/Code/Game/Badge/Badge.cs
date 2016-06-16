using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class BadgeSystemInfo
{
    private List<BadgeBook> books = null;
    public BadgeSystemInfo()
    {

    }

    public void InitBadgeBookData(List<BadgeBook> bks)
    {
        books = bks;
    }
    //添加涂鸦页
    public void AddBadgeBooks(BadgeBook data)
    {
        books.Add(data);
    }

    public BadgeBook CreateBadgeBook()
    {
        BadgeBook book = new BadgeBook();
        return book;
    }

    public BadgeSlot CreateBadgeSlot()
    {
        BadgeSlot slot = new BadgeSlot();
        return slot;
    }
    //删除涂鸦页
    public void RemoveBadgeBook(uint id)
    {
        foreach (BadgeBook book in books)
        {
            if (book.id == id)
            {
                books.Remove(book);
            }
        }
    }

    //获取指定id的涂鸦在目前所有涂鸦墙中使用的数量
    public int GetBadgeUseCountInAllSlots(uint badgeId,uint bookId)
    {
        int count = 0;
        BadgeBook book = GetBadgeBookByBookId(bookId);
        if (book != null)
        {
            foreach (BadgeSlot slot in book.slot_list)
            {
                if (slot.badge_id != 0 && slot.badge_id == badgeId)
                    count++;
            }
        }
        return count;
    }
    //除去某一页涂鸦使用后的剩余数
    public int GetBadgeleftNumExceptUsed(uint badgeId,uint bookId)
    {
        Goods tempGoods = MainPlayer.Instance.GetBadgesGoodByID(badgeId);
        if (tempGoods == null) return 0;
        uint num = tempGoods.GetNum();
        int useNum = GetBadgeUseCountInAllSlots(badgeId, bookId);
        return (int)(num - useNum);
    }
   //除去所有页涂鸦使用后的剩余数
    public int GetBadgeLeftNumExceptAllUsed(uint badgeId)
    {
        Goods tempGoods = MainPlayer.Instance.GetBadgesGoodByID(badgeId);
        if (tempGoods == null) return 0;
        int num = (int)tempGoods.GetNum();
        int useNum = GetBadgeUseCountInAllBooks(badgeId);
        int leftNum = num - useNum;
        if (leftNum < 0)
        {
            leftNum = 0;
        }
        return (int)(leftNum);
    }

    public int GetBadgeUseCountInAllBooks(uint badgeId)
    {
        int maxCount = 0;
        foreach (BadgeBook book in books)
        {
            int count = 0;
            foreach (BadgeSlot slot in book.slot_list)
            {
                if (slot.badge_id != 0 && slot.badge_id == badgeId)
                    count++;
            }
            if (count > maxCount)
            {
                maxCount = count;
            }
        }
        return maxCount;
    }
    
    public BadgeSlot GetBadgeSlotByBookIdAndSlotId(uint bookId,uint slotid)
    {
        foreach (BadgeBook book in books)
        {
            if (book.id == bookId)
            {
                foreach (BadgeSlot slot in book.slot_list)
                {
                    if (slot.id == slotid)
                    {
                        return slot;
                    }
                }
            }
        }
        return null;
    }


    public BadgeBook GetBadgeBookByBookId(uint bookid)
    {
        foreach(BadgeBook book in books)
        {
            if(book.id == bookid)
            {
                return book;
            }
        }
        return null;
    }

    public List<BadgeBook> GetAllBadgeBooks()
    {
        return books;
    }

    public uint GetAllOwnBadgeBooksNum() 
    {
        uint num = 0;
        foreach (BadgeBook badgeBook in books)
        {
            foreach (BadgeSlot badgeSlot in badgeBook.slot_list)
            {
                if (badgeSlot.status == BadgeSlotStatus.UNLOCK)
                    num += 1;
            }
        }
        return num;
    }

    public void SavePlayerUseBadgeBookId(int bookId)
    {
        foreach (BadgeBook book in books)
        {
            book.been_used = 0;
            if (book.id == bookId)
            {
                book.been_used = 1;
            }
        }
    }

    public uint GetPlayerUserBadgeBookdId()
    {
        foreach (BadgeBook book in books)
        {
            if (book.been_used>=1)
            {
                return book.id;
            }
        }
        return 1;
    }

    public int GetBookProvideTotalBadgeLevelByBookId(uint bookId)
    {
        BadgeBook book = GetBadgeBookByBookId(bookId);
        int totalLevel = 0;
        BadgeAttrBaseConfig tempConfig = null;
        if (book!=null)
        {
            foreach (BadgeSlot slot in book.slot_list)
            {
                if (slot.badge_id != 0)
                {
                    tempConfig = GameSystem.Instance.BadgeAttrConfigData.GetBaseConfig(slot.badge_id);
                    if (tempConfig != null)
                    {
                        totalLevel += (int)tempConfig.level;
                    }
                }
            }
        }
        return totalLevel;
    }
}


