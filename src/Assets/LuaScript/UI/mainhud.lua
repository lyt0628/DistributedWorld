-- local util = require 'util'
local dinames = require 'dinames'

address= 'PUI_MainHUD'
is_resident = true
need_preload = true


local ve_hpbar = nil
local ve_mpbar = nil

local ui_inventory = nil 
local playerInput = nil
local uistack = nil


local function update_hpbar()
    local combator = player.ActiveChara.Combator.Combator
    local hpbar_style = uidoc.getstyle(ve_hpbar)
    hpbar_style.width = uidoc.StyleLength(uidoc.Length(combator.Hp / combator.MaxHP * 100, uidoc.LengthUnit.Percent))
    local mpbar_style =  uidoc.getstyle(ve_mpbar)
    mpbar_style.width = uidoc.StyleLength(uidoc.Length(combator.Mana / combator.MaxMana * 100, uidoc.LengthUnit.Percent))
end

local function update_chara_listener(newchara, oldchara)
    if oldchara ~= nil then
        newchara.Combator.OnStateChanged:RemoveListener(update_hpbar)
    end
    newchara.Combator.OnStateChanged:AddListener(update_hpbar)
end

function onDocumentLoaded()
    self.Document.name = "UI_MainHUD"

    ve_hpbar =  uidoc.uquery.Q(self.Container, "hpValue")
    ve_mpbar = uidoc.uquery.Q(self.Container, "manaValue")

    ui_inventory = Trunk.Context:GetInstance(dinames.InventoryUI)
    playerInput = Trunk.Context:GetInstance(dinames.PlayerControls)
    uistack = Trunk.Context:GetInstance(dinames.UIStack)

    playerInput.MainUI.OpenInventory:started('+', function (_)
        uistack:Push(ui_inventory)
    end)

    local chara = player.ActiveChara;
    if chara ~= nil then
        update_chara_listener(chara, null)
    end
    player.CharacterChanged:AddListener(update_chara_listener);

    -- local source = TaskCompleteSource()
    -- source:SetResult(0)
    return Task.CompletedTask
end


