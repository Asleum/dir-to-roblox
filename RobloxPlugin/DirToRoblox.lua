local REQUESTS_PER_SECOND = 5
local TOOLBAR_TEXT = "DirToRoblox"
local BUTTON_ALT = "Enable or disable synchronization with DirToRoblox."
local BUTTON_TEXT = "Toggle synchronization"
local BUTTON_ICON = "rbxassetid://1507949215"
local SERVER_ERROR = "DirToRoblox plugin: couldn't connect to server. Make sur HttpService is enabled and DirToRoblox is running and active on your machine!"
local URL = "http://localhost:3260/dirtoroblox"
local HTTP = game:GetService("HttpService")
local enabled = false
local running = false
local applyEvents
applyEvents = function(events)
  for _index_0 = 1, #events do
    local event = events[_index_0]
    print(event.Type)
  end
end
local getData
getData = function()
  local response = HTTP:GetAsync(URL, true)
  return HTTP:JSONDecode(response)
end
local toggle
toggle = function(button)
  enabled = not enabled
  button:SetActive(enabled)
  if not enabled or running then
    return 
  end
  running = true
  while enabled and wait(1 / REQUESTS_PER_SECOND) do
    local success, result = pcall(getData)
    if success then
      applyEvents(result)
    else
      warn(tostring(SERVER_ERROR) .. " - " .. tostring(result))
      if enabled then
        toggle(button)
      end
      break
    end
  end
  running = false
end
local initialize
initialize = function()
  local toolbar = plugin:CreateToolbar(TOOLBAR_TEXT)
  local button = toolbar:CreateButton(BUTTON_TEXT, BUTTON_ALT, BUTTON_ICON)
  return button.Click:Connect(function()
    return toggle(button)
  end)
end
return initialize()
