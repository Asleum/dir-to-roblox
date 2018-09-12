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

--- Process a file modification
-- @tparam path

--- Apply a given list of modification to the Roblox tree
-- @param events a list of events on the local filesystem to mirror
applyEvents = (events) ->
  for event in *events
    print event.Type
    print event.Path

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
