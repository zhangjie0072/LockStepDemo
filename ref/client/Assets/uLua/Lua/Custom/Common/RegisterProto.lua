local function protos(protos)
    print('------------------------------------------')
    for _, proto in ipairs(protos) do
        if GlobalConst.IS_DEVELOP and Application.isEditor then
            parser.register(proto, Application.dataPath .. "/Proto")
        else
            local textAsset = ResourceLoadManager.Instance:GetLuaResource("Proto/" .. proto, nil, false)
            parser.register_buffer(proto, textAsset.text)
        end
    end
end
protos{
    "Base.proto",
    "QualifyingNewer.proto",
    "BullFight.proto",
    "Skill.proto",
    "Tattoo.proto",
    "Equipment.proto",
    "Goods.proto",
    "Exercise.proto",
    "Training.proto",
    "Store.proto",
    "Captain.proto",
    "Tattoo.proto",
    "Career.proto",
    "Mail.proto",
    "Practice.proto",
    "Fashion.proto",
    "Guide.proto",
    "Task.proto",
    "NewComer.proto",
    "Tour.proto",
    "Activity.proto",
    "Badge.proto",
    "ActivityEx.proto",
    "Player.proto",
    "Rank.proto",
    "Login.proto",
    "Practice.proto",
    "Qualifying.proto",
    "Regular.proto",
    "Room.proto",
    "BullFight.proto",
    "Shoot.proto",
    "Ladder.proto",
    "Match.proto",
    "Role.proto",
    "Lottery.proto",
    "Game.proto",
    "Chat.proto",
    "GMCommond.proto",
    "Friend.proto",
    "MatchEx.proto",
    "ChatEx.proto",
    "TaskLevel.proto",
    "TourNew.proto",
    "FunctionSwitch.proto"
}

--[[
for k, v in pairs(parser.all) do
    if string.match(v, ".*StartSectionMatch$") then
        local field = k.field
        for k1, v1 in pairs(field) do
            print(k1, v1)
            for k2, v2 in pairs(v1) do
                print("\t", k2, v2)
            end
        end
    end
end
--]]
