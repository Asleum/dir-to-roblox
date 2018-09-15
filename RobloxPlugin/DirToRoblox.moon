-- Constants
REQUESTS_PER_SECOND = 5
TOOLBAR_TEXT = "DirToRoblox"
BUTTON_ALT = "Enable or disable synchronization with DirToRoblox."
BUTTON_TEXT = "Toggle synchronization"
BUTTON_ICON = "rbxassetid://1507949215"
SERVER_ERROR = "DirToRoblox plugin: couldn't connect to server. Make sur HttpService is enabled and DirToRoblox is running and active on your machine"
URL = "http://localhost:3260/dirtoroblox"
HTTP = game\GetService "HttpService"

-- Variables
enabled = false
running = false

--- Destroy the given item, properly handling services (clear their children instead)
-- @tparam Instance item the item to destroy
safeDestroy = (item) ->
  success = pcall -> item\Destroy!
  if not success -- We've got a non-deletable item, just destroy its children instead
    safeDestroy child for child in *item\GetChildren!

--- Finds a child with a given name and class name
-- @tparam Instance parent the parent item in whoch to look for
-- @tparam string name the name of the child to find
-- @tparam string className the found child must have this class name
-- @treturn Instance the found item
findChild = (parent, name, className) ->
  for item in *parent\GetChildren!
    -- Folders are actually associated to anything that's not a script
    if className == "Folder"
      -- Bellow pcall is necessary because some services will error when accessesing their ClassName
      success, result = pcall -> item.Name == name and not item\IsA "LuaSourceContainer"
      return item if success and result
    else -- For anything not a folder (i.e. a script) we're looking for the exact ClassName
      success, result = pcall -> item.Name == name and item\IsA className
      return item if success and result

--- Associates a path on the filesystem to a Roblox path
-- @tparam string path the string representation of the path to convert
-- @tparam string className the last element of the path must have this class name
-- @tparam boolean create if true and the path does not exist, create it
-- @treturn Instance the corresponding Roblox path
getRobloxPath = (path, className, create) ->
  location = game -- game is our starting point
  -- This will iterate over every elements of the path except the last one
  for element in string.gmatch path, "(.-)\\"
    if child = findChild location, element, "Folder" -- Already in the Roblox path
      location = child
    else -- Not in the Roblox path
      return if not create
      -- We need to create an intermediate subdirectory
      location = with Instance.new "Folder"
        .Name = element
        .Parent = location
  -- Now we need to process the last element
  lastElement = (string.match path, "\\([^\\]-)$") or path
  if child = findChild location, lastElement, className
    return child
  return if not create
  with Instance.new className
    .Name = lastElement
    .Parent = location

--- Apply a single modification to the Roblox tree
-- @param event a local event to mirror
applyEvent = (event) ->
  warn "New event"
  print i, v for i, v in pairs event when v != "Content"
  switch event.Type
    when "Creation" or "Modification"
      -- The corresponding event is a file or directory creation or modification
      path = getRobloxPath event.Path, event.Class, true -- Get or create the roblox path
      if path\IsA("LuaSourceContainer") -- If it's a script, we need to update its content
        path.Source = event.Content
    when "Deletion"
      -- The corresponding event is a deletion: we find the corresponding Roblox item and destroy it
      if path = getRobloxPath event.Path, event.Class
        safeDestroy path
    when "Renaming"
      -- The corresponding event is a renaming: we find the corresponding Roblox item and change its
      -- name
      if path = getRobloxPath event.Path, event.Class
        -- There's a tricky case when we rename something to a name that already exists, but only in
        -- the Roblox tree (not in the local filesystem). Solution: the local item is now associated
        -- to this found item, instead of renaming and having two items with the same name
        if alreadyPresent = findChild path.Parent, event.NewName, path.ClassName
          for child in *path\GetChildren!
            child.Parent = alreadyPresent
          safeDestroy path
        else -- No tricky case, just perform a regular renaming
          path.Name = event.NewName

--- Apply a given list of modification to the Roblox tree
-- @param events a list of events on the local filesystem to mirror
applyEvents = (events) ->
  applyEvent event for event in *events

--- Get a list of local filesystem events that happened since the last check
-- @return a table of dictionnaries, each corresponding to an event
getData = ->
  response = HTTP\GetAsync URL, true
  HTTP\JSONDecode response

--- Called when the plugin button is pressed.
-- @tparam Instance button the plugin button
toggle = (button) ->
  enabled = not enabled
  button\SetActive enabled
  return if not enabled or running
  running = true
  while enabled and wait 1 / REQUESTS_PER_SECOND
    success, result = pcall getData
    if success then applyEvents result
    else
      warn "#{SERVER_ERROR} - #{result}"
      toggle button if enabled
      break
  running = false

--- Create the toolbar and its button and start reacting to clicks
initialize = ->
  toolbar = plugin\CreateToolbar TOOLBAR_TEXT
  button = toolbar\CreateButton BUTTON_TEXT, BUTTON_ALT, BUTTON_ICON
  button.Click\Connect(-> toggle button)


initialize! -- Heeeere we gooo !
