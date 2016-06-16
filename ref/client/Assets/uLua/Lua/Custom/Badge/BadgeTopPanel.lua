BadgeTopPanel = 
{
	uiName = "BadgeTopPanel",
	-----params--------------
	parent,
	-----UI For Top Area-----
	uiChangeBooksPanelNameBtn,
	uiAddBooksPanelBtn,
	uiBooksPageDropDownList,
	uiChangeBooksNameArrowBtn,
	uiMask,
	uiBookNameItem,
}

function BadgeTopPanel:Awake( ... )
	self:UIParse()
end
---------------Start--------------
function BadgeTopPanel:Start( ... )
	self:AddEvent()
	self:UpdateNameDropDownList()
	-- local allBooks = BadgeSystemInfo:GetAllBadgeBooks()
	-- if allBooks.Count >=10 then
	-- 	self.uiAddBooksPanelBtn.isEnabled = false
	-- end
end

--------------Update-----------------
function BadgeTopPanel:FixedUpdate( ... )
	-- body
end

--------------OnDestory-----------------
function BadgeTopPanel:OnDestroy( ... )
	-- body
end

function BadgeTopPanel:UIParse( ... )
	local transform = self.transform
	local find = function(struct)
		return transform:FindChild(struct)
	end

	self.uiChangeBooksPanelNameBtn = find("ChangeName"):GetComponent("UIButton")
	self.uiAddBooksPanelBtn = find("Add"):GetComponent("UIButton")
	self.uiBooksPageDropDownList = find("DropDown")
	self.uiChangeBooksNameArrowBtn = find("Arrow"):GetComponent("UIButton")

	self.uiMask = find("DropDown/Mask")
	self.uiBookNameItem = getLuaComponent(createUI("DropDownListItem",transform:FindChild("Node").transform))
	-- Obj.SetActive(self.uiBookNameItem.transform:GetComponent("BoxCollider"),false)
	self.uiBookNameItem.transform:GetComponent("BoxCollider").enabled = false
end

function BadgeTopPanel:AddEvent( ... )
	-- body
	addOnClick(self.uiChangeBooksPanelNameBtn.gameObject,self:OnChangeBooksPanelName())
	addOnClick(self.uiAddBooksPanelBtn.gameObject,self:OnAddBooksPanel())
	addOnClick(self.uiChangeBooksNameArrowBtn.gameObject,self:OnClickBooksNameArrBtn())
	addOnClick(self.uiMask.gameObject,self:CloseNameListPanel())
end

function BadgeTopPanel:CloseNameListPanel( ... )
	return function()
		-- print("当前页码是：*********"..BadgeSystemVar.currentBookId)
		-- self.uiBookNameLabel.text = BadgeSystemInfo:GetBadgeBookByBookId(BadgeSystemVar.currentBookId).name
		self:UpdateNameDropDownList()
		self:OnClickBooksNameArrBtn()()
	end
end


function BadgeTopPanel:UpdateNameDropDownList()
	CommonFunction.ClearGridChild(self.uiBooksPageDropDownList.transform:FindChild("DropGrid").transform)
	local booksList = BadgeSystemInfo:GetAllBadgeBooks()
	local count = booksList.Count
	-- print("CountName",booksList:get_Item(1).name)
	local listNode  = self.uiBooksPageDropDownList.transform:FindChild("DropGrid").transform
	local widget = self.uiBooksPageDropDownList:GetComponent("UIWidget")
	widget.height = listNode:GetComponent("UIGrid").cellHeight*count
	for i=1,count do
		local bookData = booksList:get_Item(i-1)
		if bookData then 
			local item = getLuaComponent(createUI("DropDownListItem",listNode))
			item:SetLabelText(bookData.name)
			item:SetIndex(i)
			item:SetId(bookData.id)
			item:SetNum(BadgeSystemInfo:GetBookProvideTotalBadgeLevelByBookId(bookData.id))
			addOnClick(item.gameObject,self:OnSelectBookPage())
		end
	end
	listNode.transform:GetComponent("UIGrid"):Reposition()
end

function BadgeTopPanel:OnSelectBookPage( ... )
	return function(go)
		local t = getLuaComponent(go)
		print(t:GetId())
		if t:GetId() == BadgeSystemVar.currentBookId then
			return 
		else
			self:SetCurrentBookId(t:GetId())
		end
		self:OnClickBooksNameArrBtn()()
	end
end

function BadgeTopPanel:OnChangeBooksPanelName( ... )
	return function()
		local window = createUI("BadgeChangeNameWindow")
		local t = getLuaComponent(window)
		t.okClickCallback = self:SendChangeNameRequest()
		local book = BadgeSystemInfo:GetBadgeBookByBookId(BadgeSystemVar.currentBookId)
		if book then
			t:SetInputDefaultValue(book.name)
		end
		UIManager.Instance:BringPanelForward(window)
	end
end

function BadgeTopPanel:SendChangeNameRequest()
	-- body
	return function(str)
		str = tostring(str)
		local req = {
            book_id  = BadgeSystemVar.currentBookId,
            change_name = str
        }
        
	    local buf = protobuf.encode("fogs.proto.msg.BadgeBookChangeNameReq", req)
	    LuaHelper.SendPlatMsgFromLua(MsgID.BadgeBookChangeNameReqID, buf)
	    CommonFunction.ShowWait()
	    LuaHelper.RegisterPlatMsgHandler(MsgID.BadgeBookChangeNameRespID,self:BadgeBookChangeNameHanlder(),self.uiName)
	end
end

function BadgeTopPanel:OnAddBooksPanel( ... )
	return function(go)
		local allBooks = BadgeSystemInfo:GetAllBadgeBooks()
		if allBooks.Count >= 10 then
			CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("BADGE_BOOK_NUM_REACH_MAX"),nil,nil,nil,nil,nil)
			return
		end
		local window = createUI("BadgeSlotRelativeWindow")
		local t = getLuaComponent(window)
		t:ShowAddNewPanel()
		t.addBookPageCallBack = self:AddBookPanelPageResq()
		UIManager.Instance:BringPanelForward(window)
	end
end

function BadgeTopPanel:AddBookPanelPageResq( ... )
	return function()
		local req = {}
		local buf = protobuf.encode("fogs.proto.msg.BadgeAddBookReq",req)
		CommonFunction.ShowWait()
		-- print("添加页请求")
		LuaHelper.RegisterPlatMsgHandler(MsgID.BadgeAddBookRespID,self:BadgeAddBookPageHanlder(),self.uiName)
		LuaHelper.SendPlatMsgFromLua(MsgID.BadgeAddBookReqID,buf)

	end
end

function BadgeTopPanel:OnClickBooksNameArrBtn( ... )
	return function(go)
		NGUITools.SetActive(self.uiBooksPageDropDownList.gameObject,not self.uiBooksPageDropDownList.gameObject.activeSelf)
		if self.uiBooksPageDropDownList.gameObject.activeSelf then
			self:UpdateNameDropDownList()
		end
		self.uiBooksPageDropDownList.transform:FindChild("DropGrid"):GetComponent("UIGrid"):Reposition()
	end
end

function BadgeTopPanel:SetCurrentBookId(id)
	-- body
	if BadgeBookNameUpdateCB == nil then
		BadgeBookNameUpdateCB = self:RereshCurrentPageName()
	end
	if BadgeSystemVar.currentBookId ~= id then
		BadgeSystemVar.currentBookId = id
		self:RereshCurrentPageName()()
		self.parent:ShowBooksPanel(true)
	else
		self.parent:ShowBooksPanel(true)
	end
end

function BadgeTopPanel:SetParent(parent)
	-- body
	self.parent = parent
end


function BadgeTopPanel:BadgeBookChangeNameHanlder( ... )
	return function(buf)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BadgeBookChangeNameRespID,self.uiName)
		CommonFunction.StopWait()
		print("getAwards----resp")
		local resp, err = protobuf.decode("fogs.proto.msg.BadgeBookChangeNameResp", buf)
		if resp then
			if resp.result ~= 0 then
				Debugger.Log('-----------1: {0}', resp.result)
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), self.transform)
				return
			end
			---do scuesss
			-- print("BadgeBookChangeNameResp Error!")
			-- print("成功修改涂鸦墙的名字，BOOKID:"..resp.book_id.."更改后的名字："..resp.change_name)
			local bookid = resp.book_id
			local cName = resp.change_name
			local book = BadgeSystemInfo:GetBadgeBookByBookId(bookid)
			book.name = cName
			self:UpdateNameDropDownList()
			self:RereshCurrentPageName()()
		end
	end
end

function BadgeTopPanel:RereshCurrentPageName( ... )
	-- local book = BadgeSystemInfo:GetBadgeBookByBookId(BadgeSystemVar.currentBookId)
	-- self.uiBookNameLabel.text = book.name
	return function()
		local book = BadgeSystemInfo:GetBadgeBookByBookId(BadgeSystemVar.currentBookId)
		if book~=nil then
			self.uiBookNameItem:SetLabelText(book.name)
			self.uiBookNameItem:SetNum(BadgeSystemInfo:GetBookProvideTotalBadgeLevelByBookId(BadgeSystemVar.currentBookId))
		end
	end
end

function BadgeTopPanel:BadgeAddBookPageHanlder( ... )
	return function(buf)
		LuaHelper.UnRegisterPlatMsgHandler(MsgID.BadgeAddBookRespID,self.uiName)
		CommonFunction.StopWait()
		local resp,err = protobuf.decode("fogs.proto.msg.BadgeAddBookResp",buf)
		if resp then
			if resp.result ~= 0 then
				Debugger.Log('-----------1: {0}', resp.result)
				CommonFunction.ShowErrorMsg(ErrorID.IntToEnum(resp.result), self.transform)
				return
			end
			
			-- print("添加页操作成功")
			local book = resp.book
			-- print("添加页操作成功,book的ID是"..book.id.."Name:"..book.name.."type(resp.book)"..type(resp.book))
			local csharpBook = BadgeSystemInfo:CreateBadgeBook()
			csharpBook.id = book.id
			csharpBook.name = book.name
			csharpBook.been_used = book.been_used
			for k,v in pairs(book.slot_list) do
				local slot = BadgeSystemInfo:CreateBadgeSlot()
				slot.id = v.id
				slot.badge_id = v.badge_id
				if v.status == "LOCKED" then
					slot.status = BadgeSlotStatus.LOCKED
				elseif v.status == "LOCKED_CANPRE_OPEN" then
					slot.status = BadgeSlotStatus.LOCKED_CANPRE_OPEN
				elseif v.status == "LOCKED_WILL_OPEN" then
					slot.status = BadgeSlotStatus.LOCKED_WILL_OPEN
				elseif v.status == "UNLOCK" then
					slot.status = BadgeSlotStatus.UNLOCK
				end
				csharpBook.slot_list:Add(slot)
			end
			BadgeSystemInfo:AddBadgeBooks(csharpBook)
			self:UpdateNameDropDownList()
			self:SetCurrentBookId(book.id)
			local allBooks = BadgeSystemInfo:GetAllBadgeBooks()
			CommonFunction.ShowPopupMsg(CommonFunction.GetConstString("STR_ADD_BADGE_PAGE_SUCESS_TIPS"),nil,nil,nil,nil,nil)
		end
	end
end
return BadgeTopPanel