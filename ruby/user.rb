require('json')

class User
  attr_accessor :id, :name, :city, :state

  def initialize(id, name, city, state)
    @id = id
    @name = name
    @city = city
    @state = state
  end

  def to_s
    string = "id: #{@id}\nname: #{@name}\ncity: #{@city}\nstate: #{@state}"
  end

  def to_json
    json = {"id" => @id, "name" => @name, "city" => @city, "state" => @state}.to_json
  end

  def self.from_json(json)
    hash = JSON.load json
    user = User.new(hash["id"], hash["name"], hash["city"], hash["state"])
  end
end