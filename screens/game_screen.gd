extends Node
@onready var network_manager: Node = $NetworkManager


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	network_manager.connect_to_host("127.0.0.1", 7666)
	
	await get_tree().create_timer(1).timeout
	network_manager.send_message(JSON.stringify({
		"opcode": 2
	}))


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass


func _on_network_manager_received_message(message: String) -> void:
	var packet = JSON.parse_string(message)
	var opcode = int(packet.opcode)
	
	match opcode:
		Enums.Opcode.SPAWN_CHARACTER_EVENT:
			pass
	
	pass
