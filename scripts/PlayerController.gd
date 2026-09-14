extends CharacterBody2D

@export var walk_speed := 150.0
@export var run_speed := 260.0
@export var jump_velocity := -350.0

@onready var sprite: Sprite2D = $Sprite2D
@onready var animation_player: AnimationPlayer = $AnimationPlayer

var is_dead := false

func _physics_process(delta: float) -> void:
	if is_dead:
		return

	velocity.y += get_gravity().y * delta

	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = jump_velocity

	var move_input := Input.get_axis("move_left", "move_right")
	var is_sprinting := Input.is_action_pressed("sprint")
	var current_speed := run_speed if is_sprinting else walk_speed

	velocity.x = move_input * current_speed

	if move_input > 0.01:
		sprite.flip_h = false
	elif move_input < -0.01:
		sprite.flip_h = true

	move_and_slide()

	# animation_player.play("run" if abs(velocity.x) > 0.1 else "idle")

func die() -> void:
	if is_dead:
		return
	is_dead = true
	velocity = Vector2.ZERO
	# animation_player.play("die")

	await get_tree().create_timer(2.0).timeout

	var death_screen := get_tree().get_first_node_in_group("death_screen")
	if death_screen:
		death_screen.show_screen()
