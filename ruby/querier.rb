require_relative('model_loader.rb')
class Querier

  def initialize
    @model_loader = ModelLoader.new
  end

  def get_all_from_state(state)
    users = @model_loader.load_users
    users_from_state = users.select {|u| u.state == state}
  end

  def get_num_users_messaging_during(min_hour, max_hour)
    messages = @model_loader.load_messages
    messages_in_range = messages.select { |m| m.hour >= min_hour && ( m.hour < max_hour || ( m.hour == max_hour && m.minute == 0 ) ) }
    num_users_messaging_in_range = messages_in_range.uniq { |m| m.user_id }.size
  end

end