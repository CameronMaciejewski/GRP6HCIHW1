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

  def get_users_messaging_during(min_hour, max_hour)
    messages = @model_loader.load_messages
    users = @model_loader.load_users
    messages_in_range = messages.select { |m| m.hour >= min_hour && ( m.hour < max_hour || ( m.hour == max_hour && m.minute == 0 ) ) }
    uniq_user_ids_in_range = messages_in_range.map {|m| m.user_id}.uniq
    users_in_range = users.select { |u| uniq_user_ids_in_range.include? u.id}
  end

  def get_user_max_messages_during(state, min_hour, max_hour)
    messages = @model_loader.load_messages
    users = @model_loader.load_users
    messages_in_range = messages.select { |m| m.hour >= min_hour && ( m.hour < max_hour || ( m.hour == max_hour && m.minute == 0 ) ) }
    state_users = get_all_from_state state
    user_message_counts = Array.new(state_users.size, 0)
    state_users.each_with_index do |u, i|
      if messages_in_range.any? { |m| m.user_id == u.id}
        user_message_counts[i] += 1
      end
    end
    index_max_user = user_message_counts.each_with_index.max[1]
    max_user = state_users[index_max_user]
  end

  def get_users_from_state_messaging_during(state, min_hour, max_hour)
    users_in_range = get_users_messaging_during(min_hour, max_hour)
    users_in_range_and_state = users_in_range.select { |u| u.state == state}
  end

end