require_relative('./read.rb')
require('json')

class RecordTransformer

  def initialize
    @reader = RecordReader.new
    @parent_dir = File.join(File.dirname(File.expand_path(__FILE__)), "..")
    @messages_written = 0
    @users_written = 0
  end

  def store_records(num_records)
    t1 = Time.now
    num_records.times do |n|
      record = n.to_s.rjust(6, '0')
      file_str = File.join(@parent_dir,"data","record_#{record}.dat")
      file = File.new(file_str)
      store_record file
    end
    t2 = Time.now
    delta = t2 - t1
  end

  def store_record(file)
    user, messages = @reader.read_record file

    store_user(file, user)

    store_messages(file, messages)
  end

  def store_user(file, user)
    user_json = user.to_json
    puts user_json
    user_path = File.join(@parent_dir, "customized_data", "user_#{@users_written.to_s.rjust(6, "0")}.dat")
    File.open(user_path, 'w+') {|f| f.write user_json}
    @users_written += 1
  end

  def store_messages(file, messages)
    messages.each do |msg|
      msg_json = msg.to_json
      puts msg_json
      msg_path = File.join(@parent_dir, "customized_data", "message_#{@messages_written.to_s.rjust(6, "0")}.dat")
      File.open(msg_path, 'w+') {|f| f.write msg_json}
      @messages_written += 1
    end
  end

end

t = RecordTransformer.new
delta = t.store_records(2000)
puts "took #{delta} seconds"