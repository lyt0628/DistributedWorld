local util = require 'util'


is_resident = true
need_preload = true

local option_template = nil
local option_script = nil
local option_pool = nil

local function create_option()
    return LuaUIDocument(option_template:Instantiate(), option_script, self)
end


function onDocumentLoaded()
    local load_option_template_source = TaskCompleteSource()
    local load_option_script_source = TaskCompleteSource()



--  不应该在Lua出现两层的await, 这种脏活丢给C#做
    DebugLog("Option List Loaded")
    util.await(LuaUtil.load_uidoc("uidoc_DialogPannel_Option"), function (result)
        option_template = result

        load_option_template_source:SetResult(0)
    end)

    util.await(LuaUtil.load_text("Lua_UI_DialogPanel_Option"), function (result)
        option_script = result.text
        load_option_script_source:SetResult(0)
    end)

    option_pool = ObjectPool(create_option)


    return Task.WhenAll(load_option_template_source.Task,
                        load_option_script_source.Task)
end



function render()
    -- 释放旧UI
    for i = 0, self.Container.childCount - 1 do
        local ui_option = self.Container:ElementAt(i).userdata
        option_pool:Release(ui_option)
    end
    self.Container:Clear()


    local options = self.props:Get("options")
    DebugLog(options)
    for i = 0, options.Length - 1 do -- Lua 这边必要CS的习惯调用他的迭代器
        local op = option_pool:Get()
        local op_props = CS.QS.Common.TypelessDict()
        op_props:Set("content", options[i])
        op.props = op_props
        self.Container:Add(op.Container)
    end
    -- option_pool:Release(op)

end