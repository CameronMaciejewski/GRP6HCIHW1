require('json')
require_relative('./model_loader.rb')

class RecordSorter

  def initialize
    @model_loader = ModelLoader.new
    @parent_dir = File.join(File.dirname(File.expand_path(__FILE__)), "..")
    @output_dir = File.join(@parent_dir, "customized_data")
  end

  def sort(model_to_sort, sort_row)
    if model_to_sort == "user" then
      sort_users sort_row
    else
      sort_messages sort_row
    end
  end

  def sort_messages(sort_row)
    messages = @model_loader.load_messages
    sorted = messages.sort_by &sort_row
    sorted.each_with_index do |m, i|
      m.id = i
      out_file = File.join(@output_dir, "message_#{i.to_s.rjust(6,"0")}.dat")
      msg_json = m.to_json
      puts msg_json
      File.open(out_file, 'w+') {|f| f.write msg_json}
    end
  end

  def sort_users(sort_row)
    old_new_mapping = Hash.new #TODO
    users = @model_loader.load_users
    sorted = users.sort_by &sort_row
    sorted.each_with_index do |u, i|
      old_new_mapping[u.id] = i
      u.id = i
      out_file = File.join(@output_dir, "user_#{i.to_s.rjust(6,"0")}.dat")
      user_json = u.to_json
      puts user_json
      File.open(out_file, 'w+') {|f| f.write user_json}
    end
  end

end
