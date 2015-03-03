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

end