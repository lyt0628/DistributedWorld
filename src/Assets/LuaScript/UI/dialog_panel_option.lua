
is_resident = true
need_preload = true

local lb_content = nil

function onDocumentLoaded()
    lb_content = uidoc.uquery.Q(self.Container, "optionContent")

    return Task.CompletedTask
end


function render()
    lb_content.text = self.props:Get("content")
end