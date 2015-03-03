require_relative('./read.rb')
require('yaml')

def get_yaml(messages)
  yaml = messages.map{|msg| YAML::dump(msg)[2..-1]}.join
end

def store_records(num_records)
  num_records.times do |n|
    record = n.to_s.rjust(6, '0')
    file_str = File.join(File.dirname(File.expand_path(__FILE__)), "..","data","record_#{record}.dat")
    file = File.new(file_str)
    store_record file
  end
end

def store_record(file)
  user, messages = read_record
end