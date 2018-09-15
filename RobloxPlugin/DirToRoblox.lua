local REQUESTS_PER_SECOND = 5
local TOOLBAR_TEXT = "DirToRoblox"
local BUTTON_ALT = "Enable or disable synchronization with DirToRoblox."
local BUTTON_TEXT = "Toggle synchronization"
local BUTTON_ICON = "rbxassetid://1507949215"
local SERVER_ERROR = "DirToRoblox plugin: couldn't connect to server. Make sur HttpService is enabled and DirToRoblox is running and active on your machine"
local URL = "http://localhost:3260/dirtoroblox"
local HTTP = game:GetService("HttpService")
local enabled = false
local running = false
local safeDestroy
safeDestroy = function(item)
  local success = pcall(function()
    return item:Destroy()
  end)
  if not success then
    local _list_0 = item:GetChildren()
    for _index_0 = 1, #_list_0 do
      local child = _list_0[_index_0]
      safeDestroy(child)
    end
  end
end
local findChild
findChild = function(parent, name, className)
  local _list_0 = parent:GetChildren()
  for _index_0 = 1, #_list_0 do
    local item = _list_0[_index_0]
    if className == "Folder" then
      local success, result = pcall(function()
        return item.Name == name and not item:IsA("LuaSourceContainer")
      end)
      if success and result then
        return item
      end
    else
      local success, result = pcall(function()
        return item.Name == name and item:IsA(className)
      end)
      if success and result then
        return item
      end
    end
  end
end
local getRobloxPath
getRobloxPath = function(path, className, create)
  local location = game
  for element in string.gmatch(path, "(.-)\\") do
    do
      local child = findChild(location, element, "Folder")
      if child then
        location = child
      else
        if not create then
          return 
        end
        do
          local _with_0 = Instance.new("Folder")
          _with_0.Name = element
          _with_0.Parent = location
          location = _with_0
        end
      end
    end
  end
  local lastElement = (string.match(path, "\\([^\\]-)$")) or path
  do
    local child = findChild(location, lastElement, className)
    if child then
      return child
    end
  end
  if not create then
    return 
  end
  do
    local _with_0 = Instance.new(className)
    _with_0.Name = lastElement
    _with_0.Parent = location
    return _with_0
  end
end
local applyEvent
applyEvent = function(event)
  warn("New event")
  for i, v in pairs(event) do
    if v ~= "Content" then
      print(i, v)
    end
  end
  local _exp_0 = event.Type
  if ("Creation" or "Modification") == _exp_0 then
    local path = getRobloxPath(event.Path, event.Class, true)
    if path:IsA("LuaSourceContainer") then
      path.Source = event.Content
    end
  elseif "Deletion" == _exp_0 then
    do
      local path = getRobloxPath(event.Path, event.Class)
      if path then
        return safeDestroy(path)
      end
    end
  elseif "Renaming" == _exp_0 then
    do
      local path = getRobloxPath(event.Path, event.Class)
      if path then
        do
          local alreadyPresent = findChild(path.Parent, event.NewName, path.ClassName)
          if alreadyPresent then
            local _list_0 = path:GetChildren()
            for _index_0 = 1, #_list_0 do
              local child = _list_0[_index_0]
              child.Parent = alreadyPresent
            end
            return safeDestroy(path)
          else
            path.Name = event.NewName
          end
        end
      end
    end
  end
end
local applyEvents
applyEvents = function(events)
  local _list_0 = events
  for _index_0 = 1, #_list_0 do
    local event = _list_0[_index_0]
    applyEvent(event)
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
