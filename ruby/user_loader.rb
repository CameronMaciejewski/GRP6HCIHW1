require_relative('user.rb')
require_relative('message.rb')

class UserLoader

  def initialize
    @parent_dir = File.join(File.dirname(File.expand_path(__FILE__)), "..")
    @output_dir =File.join(@parent_dir, "customized_data")
  end

  def load_users
    users = Array.new
    Dir.foreach(@output_dir) do |filestr|
      next if filestr == '.' or filestr == '..' or !(filestr.include? "user")
      json = File.read(File.join(@output_dir, filestr))
      user = User.from_json json
      users << user
    end
    return users
  end

  def load_messages
    messages = Array.new
    Dir.foreach(@output_dir) do |filestr|
      next if filestr == '.' or filestr == '..' or !(filestr.include? "message")
      json = File.read(File.join(@output_dir, filestr))
      message = Message.from_json json
      messages << message
    end
    return messages
  end

end