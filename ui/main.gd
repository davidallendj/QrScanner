extends PanelContainer


@onready var buttons = %Buttons
@onready var outputs = %Outputs


var server_pid: int = -1


func _ready() -> void:
	buttons.get_node("Client").pressed.connect(_start_client)
	buttons.get_node("Server").pressed.connect(_start_server)


func _start_server() -> void:
#	server_pid = OS.create_process("dotnet", ["run", "--project", "server"])
	server_pid = OS.create_process("server/bin/Debug/net7.0/server", [])
	if server_pid > 0:
		buttons.get_node("Server").set_text("Kill Server")


func _start_client() -> void:
	outputs.get_node("../../..").show()
	var output = []
#	OS.execute("dotnet", ["run", "--project", "client"], output)
	OS.execute("client/bin/Debug/net7.0/client", [], output)
	for line in output:
		print(line)
		var label = Label.new()
		label.set_text(line)
		outputs.add_child(label)


func _kill_server() -> void:
	if server_pid > 0:
		OS.kill(server_pid)
		buttons.get_node("Server").set_text("Start Server")
