local dinames = require 'dinames'
local util = require 'util'
--[[ 
Props {
    speaker => string,
    lines => string[],
    options => string[] -- 请务必按原始类型传到Lua
}
]]-- 


address= 'PUI_DialoguePannel'
is_resident = true
need_preload = true

local PRINT_INTERVAL = 0.15

local lb_content = nil
local ve_option_list = nil

local ui_option_list = nil
local player_actions = nil
local uistack = nil
local timer = nil



function loadStatesFromProps(props)
    local new_states = CS.QS.Common.TypelessDict()
    new_states:Set("text_length", 1)
    new_states:Set("current_line", 0)

    timer:Clear()
    if self.IsVisible then
        timer:Set(PRINT_INTERVAL);
    end

    -- 数据自顶向下流动
     oplist_props = CS.QS.Common.TypelessDict()
    oplist_props:Set("options", props:Get("options")[0])
    ui_option_list.props = oplist_props

    return new_states
end

function onDocumentLoaded()
    local source = TaskCompleteSource()


    self.Document.name = 'UI_DialogPannel'
    lb_content = uidoc.uquery.Q(self.Container, "content")
    ve_option_list = uidoc.uquery.Q(self.Container, "optionList")

    player_actions = Trunk.Context:GetInstance(dinames.PlayerControls)
    timer = Trunk.Context:GetInstance(dinames.Timer)
    uistack = Trunk.Context:GetInstance(dinames.UIStack)
    DebugLog(player_actions)


    player_actions.DialoguePanel.Continue:started('+', function (_)
            local new_line_index = self.states:Get("current_line") + 1
            
            if(new_line_index >= self.props:Get("lines").Length) then
                uistack:Pop()
            else
                -- 想办法让这也响应式上
                local  oplist_props = CS.QS.Common.TypelessDict()
                oplist_props:Set("options", self.props:Get("options")[new_line_index])
                ui_option_list.props = oplist_props
                
                self.states:Set("current_line", new_line_index)
                self.states:Set("text_length", 0)
            end
        end)

    timer.OnTick:AddListener(function ()
        local new_len = self.states:Get("text_length") + 1
        if new_len <= LuaUtil.strlen(self.props:Get("lines")[self.states:Get("current_line")]) then
            self.states:Set("text_length", new_len)
        end
        self:MarkDirty()
    end)

    local load_option_list_ui = LuaUIDocumentLoadOp(ve_option_list, "Lua_UI_DialogPanel_OptionList", self)
    load_option_list_ui:Invoke()
    util.await(load_option_list_ui.Task, function (result)
        ui_option_list = result
        -- DebugLog(ui_option_list)
        source:SetResult(0)
    end)



    return source.Task
end


function onActive()
    timer:Set(PRINT_INTERVAL)
    player_actions.DialoguePanel.Continue:Enable()
    PlayerUtil.FrozeCurrentCharacter()
end


function onDeactive()
    timer:Clear()
    player_actions.DialoguePanel.Continue:Disable()
    PlayerUtil.UnfrozeCurrentCharacter()
end


function render()
    local line = self.props:Get("lines")[self.states:Get("current_line")]
    local length = self.states:Get("text_length")
    lb_content.text = LuaUtil.substr(line, 1, length)
    
end